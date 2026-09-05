using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerDestroyer : MonoBehaviour
{
    public float positionZ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z >= positionZ){
            Destroy(gameObject);
        }
    }
}
