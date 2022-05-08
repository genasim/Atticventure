using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern1 : MonoBehaviour, IShooter
{
    #region Variables
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AttackPoints attackPoints;
    private Transform player;
    [SerializeField] float damage = 10f;
    [SerializeField] float bulletSpeed = 20f;

    [SerializeField] Animator animator;

    [SerializeField] AudioSource shotSFX;
    #endregion

    //BossPattern1
    public void Shoot()
    {
        GameObject bulletU = Instantiate(bulletPrefab, attackPoints.attackPointU.position, transform.rotation);
        bulletU.GetComponent<BulletScript>().damage = damage;
        bulletU.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(0,1).normalized;

        GameObject bulletD = Instantiate(bulletPrefab, attackPoints.attackPointD.position, transform.rotation);
        bulletD.GetComponent<BulletScript>().damage = damage;
        bulletD.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(0, -1).normalized;
        
        GameObject bulletL = Instantiate(bulletPrefab, attackPoints.attackPointL.position, transform.rotation);
        bulletL.GetComponent<BulletScript>().damage = damage;
        bulletL.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(-1, 0).normalized;
        
        GameObject bulletR = Instantiate(bulletPrefab, attackPoints.attackPointR.position, transform.rotation);
        bulletR.GetComponent<BulletScript>().damage = damage;
        bulletR.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(1, 0).normalized;
        
        shotSFX.Play();
    }

    [System.Serializable] private struct AttackPoints {
        public Transform attackPointU;
        public Transform attackPointD;
        public Transform attackPointL;
        public Transform attackPointR;
    }
}
