using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] internal Transform player;
    [SerializeField] internal Camera cam;
    [SerializeField] internal Transform attackPoint;

    [SerializeField] internal GameObject bulletPrefab;
    public float bulletSpeed = 35f;

    public float damage = 20f;
    private float nextTimeToAttack;
    public float attackSpeed = 1f;
    public float currentAttackSpeed;

    public float critRate = 10;
    public float critDamage = 50;
    private float critMeter;

    public AudioSource shotSFX;

    void Update()
    {
        currentAttackSpeed = attackSpeed;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDir = mousePos - (Vector2)attackPoint.position;

        if (Time.time >= nextTimeToAttack & Input.GetButton("Fire1"))
        {
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot(attackDir);
        }
    }

    void Shoot(Vector2 attackDir)
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * attackDir.normalized;
        if (critRate >= critMeter)
        {
            bullet.GetComponent<BulletScript>().damage = damage;
        }
        else
        {
            bullet.GetComponent<BulletScript>().damage = damage * (100 + critDamage / 100);
        }

        shotSFX.Play();
    }
}
