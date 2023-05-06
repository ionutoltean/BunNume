using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayFireHandler : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayer;
    [SerializeField] private float _damageAmount = 30f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (ReferenceEquals(currentPlayer, col.gameObject)) return;

        PlayerHealth currentPlayerHealth = col.gameObject.GetComponent<PlayerHealth>();
        if (currentPlayerHealth != null)
        {
            currentPlayerHealth.TakeDamage(_damageAmount);
        }
    }
}
