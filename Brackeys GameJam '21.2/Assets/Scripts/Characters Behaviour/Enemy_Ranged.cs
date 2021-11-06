using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPoint;

    private float nextTimetoAttack = 2f;
    public float attackSpeed = 0.4f;

    public float damage = 1f;
    public float bulletSpeed = 20f;

    public AudioSource shotSFX;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = player;

        Vector2 attackDir = player.position - gameObject.transform.position;

        if (Time.time >= nextTimetoAttack)
        {
            Shoot(attackDir);
            nextTimetoAttack = Time.time + 1 / attackSpeed;
        }
    }

    void Shoot(Vector2 attackDir)
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, transform.rotation);
        bullet.GetComponent<BulletScript>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * attackDir.normalized;
        shotSFX.Play();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
    }
}
