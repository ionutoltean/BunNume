using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f; 
    [SerializeField] private float moveAmount = 0.1f;

    public float timeToDestroy = 1f;
    public Transform moveToTarget;
    public ParticleSystem breakParticle;
    public CircleCollider2D obstacleCollider;
    public SpriteRenderer obstacleRenderer;

    private Vector3 _moveToPosition = new Vector3(0,0,0);
    private bool _shouldMove = true;

    private void Start()
    {
        _moveToPosition = moveToTarget.transform.position;
    }

    private void Update()
    {
        if(_shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveToPosition, moveAmount);
        
            if (transform.position == _moveToPosition)
            {
                _shouldMove = false;
                breakParticle.Play();
                obstacleCollider.enabled = false;
                obstacleRenderer.enabled = false;
                Invoke(nameof(DestroyObstacle), timeToDestroy);
            }
        }
    }

    private void DestroyObstacle()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHealth currentPlayerHealth = col.gameObject.GetComponent<PlayerHealth>();
        if (currentPlayerHealth != null)
        {
            currentPlayerHealth.TakeDamage(damageAmount);
        }
    }
}
