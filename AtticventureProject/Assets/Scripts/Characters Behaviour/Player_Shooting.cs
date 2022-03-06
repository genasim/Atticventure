using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_Shooting : MonoBehaviour, IShooter
{
#region Variables
    private InputSystem playerInput;
    // private PlayerInput playerInput;

    private Camera cam;
    private ButtonHeld onScreenAttackButton;
    [SerializeField] private Transform attackPoint;
    public static RoomManager currentRoom;
    private Transform nearestEnemy;
    private GameObject[] enemiesInRoom;

    [SerializeField] private GameObject bulletPrefab;

    public Vector2 attackDir {get; private set;}
    private bool attackHeld;
    public float bulletSpeed = 35f;
    public float damage = 20f;
    public float nextTimeToAttack;
    public float attackSpeed = 1f;
    public float currentAttackSpeed;

    public float critRate = 10;
    public float critDamage = 50;
    private readonly float critMeter;

    public AudioSource shotSFX;
#endregion


    private void Awake() {
        // TODO: Reverted becuase of incompatability between On-Screen Controls and PlayerInput component
        // playerInput = GetComponent<PlayerInput>();
        // playerInput.actions["Shoot"].performed += _ => attackHeld = true;
        // playerInput.actions["Shoot"].canceled += _ => attackHeld = false;

        onScreenAttackButton = GameObject.FindGameObjectWithTag("AttackButton").GetComponent<ButtonHeld>();
        cam = Camera.main;
        playerInput = _InitialiseInput.playerInput;
        playerInput.Player.Shoot.performed += ctx => action = ctx.action.ToString();
    }

#region "attackHeld Subscriptions"
    private void OnEnable() {
        playerInput.Player.Shoot.performed += _ => attackHeld = true;
        playerInput.Player.Shoot.canceled += _ => attackHeld = false;
    }

    private void OnDisable() {
        playerInput.Player.Shoot.performed -= _ => attackHeld = true;
        playerInput.Player.Shoot.canceled -= _ => attackHeld = false;
    }
#endregion

    InputControl control;
    string action; 
    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        attackHeld = onScreenAttackButton.pressed;
        GetNearestEnemy(out nearestEnemy);
        if (Time.time >= nextTimeToAttack && attackHeld)
        {
            attackDir = nearestEnemy.position - transform.position;
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot();
        }
#else
        // playerInput.
        // Debug.Log(control.device);
        if (Time.time >= nextTimeToAttack && attackHeld)
        {
            // attackDir = playerInput.actions["AttackDirection"].ReadValue<Vector2>();
            attackDir = playerInput.Player.AttackDirection.ReadValue<Vector2>();
            if (String.Equals(action, "Player/Shoot[/Mouse/leftButton]"))
                attackDir = cam.ScreenToWorldPoint(attackDir) - attackPoint.position;
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot();
        }
#endif
        currentAttackSpeed = attackSpeed;
    }

    private void GetNearestEnemy(out Transform nearestEnemy) {
        enemiesInRoom = currentRoom.enemyList.ToArray();
        Transform[] enemyPositions = new Transform[enemiesInRoom.Length];
        
        if (enemiesInRoom.Length != 0) {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var trans in enemiesInRoom) {
                Vector3 diff = trans.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance) {
                    closest = trans;
                    distance = curDistance;
                }
            }
            nearestEnemy = closest.transform;
        }
        else
            nearestEnemy = null;
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
