using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDissapear : MonoBehaviour
{
    private Collider platformCollider;
    private Renderer platformRenderer;

    public float dissapearDelay = 2f;
    public float respawnTime = 2f;

    void Start()
    {
        platformCollider = GetComponent<Collider>();
        platformRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Start the disappear coroutine
            StartCoroutine(DisappearTemporarily());
        }
    }

    private System.Collections.IEnumerator DisappearTemporarily()
    {
        yield return new WaitForSeconds(dissapearDelay);
        // Disable visuals and collider
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        // Wait
        yield return new WaitForSeconds(respawnTime);

        // Enable visuals and collider again
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
        gameObject.tag = "Dissapearing";
    }
}
