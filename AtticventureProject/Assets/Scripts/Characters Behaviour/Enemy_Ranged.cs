using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    private Transform thisEnemy;
    private Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPoint;

    private float nextTimetoAttack = 2f;
    private Vector2 attackDir;
    public float attackSpeed = 0.4f;

    public float damage = 10f;
    public float bulletSpeed = 20f;

    [SerializeField] Animator animator;

    [SerializeField] AudioSource shotSFX;

    void Awake()
    {
        thisEnemy = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        //if (Time.time >= nextTimetoAttack)
        //{
        //    Shoot(player.position - gameObject.transform.position);
        //    nextTimetoAttack = Time.time + 1 / attackSpeed;
        //}

        attackDir = player.position - gameObject.transform.position;

        Vector2 playerDir = PlayerDir();
        animator.SetFloat("Horizontal", playerDir.x);
        animator.SetFloat("Vertical", playerDir.y);
    }

    private Vector2 PlayerDir()
    {
        float x = Mathf.Abs(thisEnemy.position.x - player.position.x);
        float y = Mathf.Abs(thisEnemy.position.y - player.position.y);
        //print($"x:{x} y:{y}");

        if (x >= y)
            return new Vector2((thisEnemy.position.x > player.position.x) ? -1 : 1, 0);
        else
            return new Vector2(0, (thisEnemy.position.y > player.position.y) ? -1 : 1);
    }

    public void Shoot()     // Being called by AnimationEvent in ShootPistol.cs
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
