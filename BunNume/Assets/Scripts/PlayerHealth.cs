using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float _cooldownDMG = 0.5f;
    private Image _healthImage;
    private bool _canTakeDMG = true;
    private void Awake()
    {
        _canTakeDMG = true;
        _healthImage = GetComponentInChildren<Image>();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public float GetCurrentHealth() => currentHealth;
    public void SetCurrentHealth(float newValue)
    {
        currentHealth = newValue;
    }
    public void TakeDamage(float damageAmount)
    {
        if (_canTakeDMG == false)
            return;

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
        _canTakeDMG = false;
        StartCoroutine(nameof(WaitCooldownDMG));

    }
    private IEnumerator WaitCooldownDMG()
    {
        yield return new WaitForSeconds(_cooldownDMG);
        _canTakeDMG = true;
    }

    private void Die()
    {
        Debug.Log(gameObject.transform.name + " has died");
    }
}
