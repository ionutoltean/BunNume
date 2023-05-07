using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float _cooldownDMG = 0.5f;
    public AudioSource deathSound;
    public AudioSource ouchSound;
    public ParticleSystem bloodParticle;
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

        if (ouchSound)
            ouchSound.Play();

        if (_healthImage != null)
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
        if (bloodParticle)
            bloodParticle.Play();
        if (deathSound)
            deathSound.Play();
        var anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            
            anim.SetBool("Ded", true);
            var player = GetComponentInChildren<Player>();
            player.enabled = false;
        }

        Invoke(nameof(DidEveryoneDie), 0.1f);
    }

    private void DidEveryoneDie()
    {
        var ok = 0;
        foreach (var player in FindObjectsOfType<Player>())
        {
            if (player.enabled) ok++;
        }

        if (ok == 1)
        {
            Invoke(nameof(GetToEndScreen), 1);
        }
    }

    private void GetToEndScreen()
    {
        SceneManager.LoadScene(3);
    }
}