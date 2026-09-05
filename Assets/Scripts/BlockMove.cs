using System.Collections;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public float moveDistance;
    public float moveSpeed;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = false;
    private bool logicActive = false; // Flag to start movement after delay

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);

        if(moveDistance == 0){
            moveDistance = Random.Range(0f, 1.5f);
        }
        if(moveSpeed == 0){
            moveSpeed = Random.Range(0f, 1.5f);
        }
        StartCoroutine(StartAfterDelay(Random.Range(0f, 1.5f)));
    }

    IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        logicActive = true;
    }

    void Update()
    {
        if (!logicActive) return;

        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingUp = false;
                targetPosition = startPosition - new Vector3(0, moveDistance, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingUp = true;
                targetPosition = startPosition + new Vector3(0, moveDistance, 0);
            }
        }
    }
}
