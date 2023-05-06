using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayFireHandler : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayer;
    [SerializeField] private float _damageAmount = 10f;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (ReferenceEquals(currentPlayer, col.gameObject)) return;

        PlayerHealth currentPlayerHealth = col.gameObject.GetComponent<PlayerHealth>();
        if (currentPlayerHealth != null)
        {
            currentPlayerHealth.TakeDamage(_damageAmount);
        }
    }
}