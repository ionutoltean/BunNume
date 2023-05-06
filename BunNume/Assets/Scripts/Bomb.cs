using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //Settings
    [Header("Settings")]
    [SerializeField] private float damageAmount = 100f;
    [SerializeField] private float timeToExplode = 1f;
    [SerializeField] private float timeToDestroy = 1f;
    
    //Components
    [Header("Components")]
    public ParticleSystem explosionParticle;
    public SpriteRenderer bombSprite;
    public Collider2D bombCollider;
    
    //Players
    [SerializeField] private List<PlayerHealth> playerHealths = new List<PlayerHealth>();
    void Start()
    {
        Invoke(nameof(Explode), timeToExplode);
    }

    private void Explode()
    {
        foreach (var playerHealth in playerHealths)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        explosionParticle.Play();
        bombSprite.enabled = false;
        bombCollider.enabled = false;
        
        Invoke(nameof(DestroyMe), timeToDestroy);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerHealth currentPlayerHealth = coll.GetComponent<PlayerHealth>();
        if (currentPlayerHealth != null)
        {
            playerHealths.Add(currentPlayerHealth);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerHealth currentPlayerHealth = coll.GetComponent<PlayerHealth>();
        if (playerHealths.Contains(currentPlayerHealth))
        {
            playerHealths.Remove(currentPlayerHealth);
        }
    }
}
