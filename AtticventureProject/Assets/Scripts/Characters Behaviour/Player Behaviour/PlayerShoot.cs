using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeGeneration;

[RequireComponent(typeof(HealthManager))]
public abstract class PlayerShoot : MonoBehaviour, IShooter
{
    protected static PlayerData data;
    public static RoomManager currentRoom;


    protected Transform attackPoint;
    protected Camera cam;


    protected bool attackHeld = false;
    public Vector2 attackDir {get; set;}
    protected float nextTimeToAttack;
    protected float currentAttackSpeed = 1f;

    [SerializeField] protected AudioSource shotSFX;

    virtual protected void Awake() {
        data = PlayerManager.Instance.data;
        shotSFX = GetComponent<AudioSource>();
        attackPoint = gameObject.transform.GetChild(0).transform;
        cam = Camera.main;
    }
    abstract protected void OnEnable();
    abstract protected void OnDisable();
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(data.Bullet, attackPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = data.BulletSpeed * attackDir.normalized;
        var value = UnityEngine.Random.Range(1, 100);
        if (value > data.CritRate)
        {
            bullet.GetComponent<BulletScript>().damage = data.Damage;
        }
        else
        {
            bullet.GetComponent<BulletScript>().damage = data.Damage + data.CritDamage / 10;
            bullet.GetComponent<SpriteRenderer>().color = Color.red;
        }

        shotSFX.Play();
    }
}
