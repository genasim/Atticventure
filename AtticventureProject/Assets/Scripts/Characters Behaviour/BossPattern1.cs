using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class BossPattern1 : MonoBehaviour, IShooter
{
    #region Variables
    private Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPointU;
    [SerializeField] Transform attackPointD;
    [SerializeField] Transform attackPointL;
    [SerializeField] Transform attackPointR;

    public Vector2 attackDir { get; private set; }

    public float damage = 10f;
    public float bulletSpeed = 20f;

    [SerializeField] Animator animator;

    [SerializeField] AudioSource shotSFX;
    #endregion

    //BossPattern1
    public void Shoot()
    {
        GameObject bulletU = Instantiate(bulletPrefab, attackPointU.position, transform.rotation);
        bulletU.GetComponent<BulletScript>().damage = damage;
        bulletU.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(0,1).normalized;
        GameObject bulletD = Instantiate(bulletPrefab, attackPointD.position, transform.rotation);
        bulletD.GetComponent<BulletScript>().damage = damage;
        bulletD.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(0, -1).normalized;
        GameObject bulletL = Instantiate(bulletPrefab, attackPointL.position, transform.rotation);
        bulletL.GetComponent<BulletScript>().damage = damage;
        bulletL.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(-1, 0).normalized;
        GameObject bulletR = Instantiate(bulletPrefab, attackPointR.position, transform.rotation);
        bulletR.GetComponent<BulletScript>().damage = damage;
        bulletR.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(1, 0).normalized;
        shotSFX.Play();
    }
}
