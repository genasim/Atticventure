using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
<<<<<<< Updated upstream:AtticventureProject/Assets/Scripts/Characters Behaviour/Shooting.cs
    [SerializeField] private Transform player;
=======
    //private PlayerInput playerInput; TODO Mouse delta
>>>>>>> Stashed changes:AtticventureProject/Assets/Scripts/Characters Behaviour/Player_Shooting.cs
    [SerializeField] private Camera cam;

    [SerializeField] private Transform attackPoint;

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

    void Update()
<<<<<<< Updated upstream:AtticventureProject/Assets/Scripts/Characters Behaviour/Shooting.cs
    {
        currentAttackSpeed = attackSpeed;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDir = mousePos - (Vector2)attackPoint.position;

        if (Time.time >= nextTimeToAttack & Input.GetButton("Fire1"))
=======
    {
        currentAttackSpeed = attackSpeed;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attackDir = mousePos - (Vector2)attackPoint.position;
    }

    public void DoShoot()
    {
        if (Time.time >= nextTimeToAttack)
>>>>>>> Stashed changes:AtticventureProject/Assets/Scripts/Characters Behaviour/Player_Shooting.cs
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

