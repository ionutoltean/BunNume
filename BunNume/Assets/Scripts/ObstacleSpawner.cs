using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private float portalOpenTimer = 1f;
    public GameObject obstaclePrefab;
    public Transform spawnPointTransform;
    public Transform moveTowardsPointTransform;
    public ParticleSystem portalParticle;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnParticle), spawnTimer,spawnTimer);
    }
    
    private void SpawnParticle()
    {
        portalParticle.Play();
        Invoke(nameof(SpawnObstacle), portalOpenTimer);
    }
    private void SpawnObstacle()
    {
        GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnPointTransform.position, Quaternion.identity);
        spawnedObstacle.GetComponent<Obstacle>().moveToTarget = moveTowardsPointTransform;
    }

}
