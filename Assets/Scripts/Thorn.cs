using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public GameObject thorn;
    public GameObject ball;

    private Vector3 ballPosition;
    private int[] cubesN = new int[]{2,5,8,11,14,17,20,23,29,32,35,40,43,46};
    private Transform[] cubes;
    private GameObject[] thorns;

    void Start()
    {
        ballPosition = ball.transform.position;
        cubes = new Transform[cubesN.Length];
        thorns = new GameObject[cubesN.Length];
        for(int i = 0; i < cubesN.Length; i++){
            cubes[i] = transform.Find("Cube (" + cubesN[i] + ")");
        }
    }

    void Update()
    {
        ballPosition = ball.transform.position;
        for(int i = 0; i < cubes.Length; i++){
            float distance = Vector3.Distance(cubes[i].position, ballPosition);
            if(distance < 3f){
                if(thorns[i] != null) continue;
                int random = Random.Range(0,2);
                Debug.Log(i + " " + random);
                if(random == 0){
                    GameObject th = Instantiate(thorn, cubes[i]);
                    th.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                    th.transform.Rotate(0f, -90f, 0f);
                    thorns[i] = th;
                }else{
                    thorns[i] = ball;
                }
            }
            if(distance > 5f){
                if(thorns[i]){
                    Debug.Log("destroy");
                    if(thorns[i] != ball){
                        Destroy(thorns[i]);
                    }
                    thorns[i] = null;
                }
            }
        }
    }
}
