using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 10f;
    public GameObject obstaclePrefab;
    public Transform spawnPointTransform;
    public Transform moveTowardsPointTransform;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnOObstacle), spawnTimer,spawnTimer);
    }

    private void SpawnOObstacle()
    {
        GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnPointTransform.position, Quaternion.identity);
        spawnedObstacle.GetComponent<Obstacle>().moveToTarget = moveTowardsPointTransform;
    }
}
