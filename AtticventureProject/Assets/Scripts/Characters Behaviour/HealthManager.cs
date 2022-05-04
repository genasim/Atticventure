using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class HealthManager : MonoBehaviour
{
    [SerializeField] bool gameOver = false;

    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private AudioSource hitSFX;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        hitSFX.Play();

        StartCoroutine(HitEffect());

        if (currentHealth <= 0) 
            Die();

        print($"damage taken {maxHealth} {currentHealth}");
    }

    IEnumerator HitEffect() {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();

        var color = Color.red;
        sprite.color = color;
        while (color.g < 1 && color.b < 1) {    
            color.g += .007f;
            color.b += .007f;
            sprite.color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        if (gameOver) SceneManager.LoadScene(0);
    }
}
