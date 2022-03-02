using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour, IShooter
{
#region Variables
    private InputSystem playerInput;
    // private PlayerInput playerInput;

    private Camera cam;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private GameObject bulletPrefab;

    public Vector2 attackDir {get; private set;}
    private bool attackHeld;
    public float bulletSpeed = 35f;
    public float damage = 20f;
    private float nextTimeToAttack;
    public float attackSpeed = 1f;
    public float currentAttackSpeed;

    public float critRate = 10;
    public float critDamage = 50;
    private readonly float critMeter;

    public AudioSource shotSFX;
#endregion
    string action; 

    private void Awake() {
        // TODO: Reverted becuase of incompatability between On-Screen Controls and PlayerInput component
        // playerInput = GetComponent<PlayerInput>();
        // playerInput.actions["Shoot"].performed += _ => attackHeld = true;
        // playerInput.actions["Shoot"].canceled += _ => attackHeld = false;

        cam = Camera.main;
        playerInput = _InitialiseInput.playerInput;
        playerInput.Player.Shoot.performed += ctx => action = ctx.action.ToString();
    }
    
    private void OnEnable() {
        playerInput.Player.Shoot.performed += _ => attackHeld = true;
        playerInput.Player.Shoot.canceled += _ => attackHeld = false;
    }

    private void OnDisable() {
        playerInput.Player.Shoot.performed -= _ => attackHeld = true;
        playerInput.Player.Shoot.canceled -= _ => attackHeld = false;
    }

    void Update()
    {
        Debug.Log(action);
        if (Time.time >= nextTimeToAttack && attackHeld)
        {
            // attackDir = playerInput.actions["AttackDirection"].ReadValue<Vector2>();
            attackDir = playerInput.Player.AttackDirection.ReadValue<Vector2>();
            if (String.Equals(action, "Player/Shoot[/Mouse/leftButton]"))
                attackDir = cam.ScreenToWorldPoint(attackDir) - attackPoint.position;
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot();
        }
        currentAttackSpeed = attackSpeed;
    }

    public void Shoot()
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
