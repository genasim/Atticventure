using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource hitSFX;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        hitSFX.PlayDelayed(.1f);

        // if (currentHealth <= 0) 
        //     Die();

        print($"damage taken {maxHealth} {currentHealth}");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
