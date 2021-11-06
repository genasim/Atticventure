using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPoint;

    public float damage = 1f;
    public float bulletSpeed = 20f;
    private float nextTimetoAttack = 2f;
    public float attackSpeed = 0.4f;

    private void Update()
    {
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
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * attackDir.normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextTimetoAttack = Time.time + 1 / attackSpeed;
            if (Time.time >= nextTimetoAttack)
                collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("enemy in room!");
        //RoomManager.enemyList.Add(gameObject);
        //print("enemy added");
    }

}
