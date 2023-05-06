using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    private Image _healthImage;

    private void Awake()
    {
        _healthImage = GetComponentInChildren<Image>();
    }

    public float GetCurrentHealth() => currentHealth;
    public void SetCurrentHealth(float newValue)
    {
        currentHealth = newValue;
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        _healthImage.fillAmount = currentHealth / 100;

    }

    private void Die()
    {
        Debug.Log(gameObject.transform.name + " has died");
    }
}
