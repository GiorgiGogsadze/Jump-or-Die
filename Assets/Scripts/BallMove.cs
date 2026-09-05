using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMove : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    private float jumpForce = 5f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 startPosition;

    private AudioSource JumpAudio;
    public AudioSource backgroundMusic;

    private bool onMovingPlatform = false;

    private Texture2D originalTex;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalTex = mat.mainTexture as Texture2D;
        
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        JumpAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveBall();
        DetectJump();
        DetectDeath();
        CheckTimer();
    }

    void MoveBall(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // this is to know which side does camera watch and determine which side is forward and right
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Keep only the horizontal components of the camera's directions
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction relative to the camera
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized;
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
        rb.AddForce(movement * moveSpeed);
    }

    void DetectJump(){
        // Debug.Log(Mathf.Abs(rb.velocity.y));
        if (Input.GetButtonDown("Jump") && (Mathf.Abs(rb.linearVelocity.y) < 0.3f || onMovingPlatform) && !GameManager.isPaused)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            JumpAudio.Play();
            GameManager.numberJumps++;
        }
    }

    void DetectDeath(){
        if (transform.position.y < 3f)
        {
            Death();
        }
    }

    void ReturnBall(){
        backgroundMusic.Stop();
        transform.position = startPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        moveSpeed = 1.5f;
        jumpForce = 5f;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        backgroundMusic.Play();
    }

    void Death()
    {
        ReturnBall();
        GameManager.numberDeaths++;
    }

    void TimeOver(){
        ReturnBall();
        GameManager.remainingTime = GameManager.levelInfo[GameManager.currentLevel]["time"];
    }

    void CheckTimer(){
        if (GameManager.remainingTime < 0){
            TimeOver();
        }
    }

    void FinishLevel(){
        SceneManager.LoadScene("Scenes/End");
    }

    void IncreaseSpeed(){
        moveSpeed = 2f;
        jumpForce = 7f;
    }   

    void IncreaseSize(){
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        jumpForce = 9f;
        moveSpeed = 2f;
    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Death")){
            Death();
        }
        
        if (collision.gameObject.CompareTag("Finish"))
        {
            FinishLevel();
        }

        if (collision.gameObject.CompareTag("Speeder"))
        {
            GameManager.needLine = true;
            GameObject capsule = collision.gameObject;
            capsule.GetComponent<AudioSource>().Play();
            
            StartCoroutine(DisspearCapsule(capsule, 1f));
            IncreaseSpeed();
        }

        if (collision.gameObject.CompareTag("Sizer"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            IncreaseSize();
        }

        if(collision.gameObject.CompareTag("MovingPlatform")){
            onMovingPlatform = true;
        }else{
            onMovingPlatform = false;
        }

        if(collision.gameObject.CompareTag("Dissapearing")){
            collision.gameObject.tag = "Untagged";
        }
    }

    IEnumerator DisspearCapsule(GameObject capsule, float duration){
        float fadeValue = 0f;
        Material material = capsule.GetComponent<Renderer>().material;
        while(duration > 0f){
            duration -= 0.1f;
            fadeValue += 0.1f;
            material.SetFloat("_FadeAwayValue", fadeValue);
            yield return new WaitForSeconds(0.1f);
        }

        capsule.GetComponent<Renderer>().enabled = false;
        capsule.GetComponent<Collider>().enabled = false;
    }

}

