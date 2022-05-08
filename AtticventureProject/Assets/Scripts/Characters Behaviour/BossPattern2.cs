using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class BossPattern2 : MonoBehaviour, IShooter
{
    #region Variables
    private Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPointUR;
    [SerializeField] Transform attackPointUL;
    [SerializeField] Transform attackPointDR;
    [SerializeField] Transform attackPointDL;

    public Vector2 attackDir { get; private set; }

    public float damage = 10f;
    public float bulletSpeed = 20f;

    [SerializeField] Animator animator;

    [SerializeField] AudioSource shotSFX;
    #endregion

    //BossPattern2
    public void Shoot()
    {
        GameObject bulletUR = Instantiate(bulletPrefab, attackPointUR.position, transform.rotation);
        bulletUR.GetComponent<BulletScript>().damage = damage;
        bulletUR.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(1,1).normalized;
        GameObject bulletUL = Instantiate(bulletPrefab, attackPointUL.position, transform.rotation);
        bulletUL.GetComponent<BulletScript>().damage = damage;
        bulletUL.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(-1, 1).normalized;
        GameObject bulletDR = Instantiate(bulletPrefab, attackPointDR.position, transform.rotation);
        bulletDR.GetComponent<BulletScript>().damage = damage;
        bulletDR.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(1, -1).normalized;
        GameObject bulletDL = Instantiate(bulletPrefab, attackPointDL.position, transform.rotation);
        bulletDL.GetComponent<BulletScript>().damage = damage;
        bulletDL.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(-1, -1).normalized;
        shotSFX.Play();
    }
}
