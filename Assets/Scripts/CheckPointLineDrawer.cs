using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointLineDrawer : MonoBehaviour
{
    public Transform checkpointA;
    public Transform checkpointB;
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();    
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.needLine) return;
        if(checkpointA == null || checkpointB == null) return;
        if(checkpointA.position.z <= checkpointB.position.z){
            GameManager.needLine = false;
            Destroy(gameObject);
        }
        
        line.SetPosition(0, checkpointA.position);
        line.SetPosition(1, checkpointB.position);
        
    }
}
