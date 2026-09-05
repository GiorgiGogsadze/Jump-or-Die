using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlatform : MonoBehaviour
{
    public GameObject platform;

    void Start()
    {
        platform.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            platform.SetActive(true);
        }
    }
}
