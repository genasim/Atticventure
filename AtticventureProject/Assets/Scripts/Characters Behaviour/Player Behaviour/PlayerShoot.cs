using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthManager))]
public abstract class PlayerShoot : MonoBehaviour, IShooter
{
    public static PlayerData data;
    public static RoomManager currentRoom;


    protected Transform attackPoint;
    protected Camera cam;


    protected bool attackHeld = false;
    public Vector2 attackDir {get; set;}
    protected float nextTimeToAttack;
    public float currentAttackSpeed = 1f;
    protected float critMeter {get;}


    [SerializeField] protected AudioSource shotSFX;

    protected abstract void OnEnable();
    protected abstract void OnDisable();
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(data.bullet, attackPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = data.bulletSpeed * attackDir.normalized;
        
        if (data.critRate >= critMeter)
        {
            bullet.GetComponent<BulletScript>().damage = data.damage;
        }
        else
        {
            bullet.GetComponent<BulletScript>().damage = data.damage * (100 + data.critDamage / 100);
        }

        shotSFX.Play();
    }
}
