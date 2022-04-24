using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy_Ranged : MonoBehaviour, IShooter
{
#region Variables
    private Transform player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform attackPoint;

    public Vector2 attackDir{get; private set;}
    private Vector2 playerDir;

    public float damage = 10f;
    public float bulletSpeed = 20f;

    [SerializeField] Animator animator;

    [SerializeField] AudioSource shotSFX;
#endregion

    void Awake()
    {
        player = PlayerManager.Instance.Player.transform;
    }


    void Update()
    {
        if (!player) return;

        attackDir = player.position - gameObject.transform.position;
        PlayerDir(out playerDir);
        animator.SetFloat("Horizontal", playerDir.x);
        animator.SetFloat("Vertical", playerDir.y);
    }

    private void PlayerDir(out Vector2 playerDir)
    {
        float x = Mathf.Abs(transform.position.x - player.position.x);
        float y = Mathf.Abs(transform.position.y - player.position.y);
        // print($"x:{x} y:{y}");

        if (x >= y)
            playerDir = new Vector2((transform.position.x > player.position.x) ? -1 : 1, 0);
        else
            playerDir = new Vector2(0, (transform.position.y > player.position.y) ? -1 : 1);
    }

    public void Shoot()     // Called via AnimationEvent in AnimEvent_HolyHunter.cs
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
