using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shooting : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform attackPoint;
    private Vector2 attackDir;

    [SerializeField] private GameObject bulletPrefab;
    public float bulletSpeed = 35f;

    public float damage = 20f;
    private float nextTimeToAttack;
    public float attackSpeed = 1f;
    public float currentAttackSpeed;

    public float critRate = 10;
    public float critDamage = 50;
    private readonly float critMeter;

    public AudioSource shotSFX;
    
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    public void DoShoot()
    {
        if (Time.time >= nextTimeToAttack)
        {
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot(attackDir);
        }
    }


    void Update()
    {
        currentAttackSpeed = attackSpeed;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attackDir = mousePos - (Vector2)attackPoint.position;
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
