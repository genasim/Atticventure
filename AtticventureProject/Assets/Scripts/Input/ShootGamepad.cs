using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootGamepad : PlayerShoot
{
    private InputGamepad input;

    private ButtonHeld onScreenAttackButton;
    [SerializeField] private Transform nearestEnemy;
    private GameObject[] enemiesInRoom;

    protected override void OnEnable() {
        input.Player.Shoot.performed += _ => attackHeld = true;
        input.Player.Shoot.canceled += _ => attackHeld = false;
    }
    protected override void OnDisable() {
        input.Player.Shoot.performed -= _ => attackHeld = true;
        input.Player.Shoot.canceled -= _ => attackHeld = false;
    }

    private void Awake() {
        onScreenAttackButton = GameObject.FindGameObjectWithTag("AttackButton").GetComponent<ButtonHeld>();
        input = PlayerManager.inputGamepad;
        shotSFX = GetComponent<AudioSource>();
        attackPoint = gameObject.transform.GetChild(0).transform;
        cam = Camera.main;
    }

    private void Update() {
        attackHeld = onScreenAttackButton.pressed;
        GetNearestEnemy(out nearestEnemy);
        if (Time.time >= nextTimeToAttack && attackHeld)
        {
            attackDir = nearestEnemy.position - transform.position;
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot();
        }
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
}
