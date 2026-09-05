using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public GameObject roller;
    public Vector2 spawnTimeRange;
    public Vector2 spawnPosition;
    private float timer = 0f;
    private float spawnDelay;

    void Start()
    {
        spawnDelay = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnDelay){
            float x = new float[]{10f, 8f, 6f}[Random.Range(0,3)];
            Instantiate(roller, new Vector3(x, spawnPosition.x, spawnPosition.y), Quaternion.identity);
            timer = 0f;
            spawnDelay = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }
    }
}
