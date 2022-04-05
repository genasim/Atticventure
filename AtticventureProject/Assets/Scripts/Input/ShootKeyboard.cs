using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootKeyboard : PlayerShoot
{
    private InputKeyboard input;
    private Vector2 mousePos;

    protected override void Awake() {
        base.Awake();
        input = PlayerManager.Instance.inputKeyboard;
    }

    protected override void OnEnable() {
        input.Player.Shoot.performed += _ => attackHeld = true;
        input.Player.Shoot.canceled += _ => attackHeld = false;
    }

    protected override void OnDisable() {
        input.Player.Shoot.performed -= _ => attackHeld = true;
        input.Player.Shoot.canceled -= _ => attackHeld = false;
    }

    private void Update() {
        mousePos = input.Player.AttackDirection.ReadValue<Vector2>();
        if (Time.time >= nextTimeToAttack && attackHeld)
        {
            attackDir = cam.ScreenToWorldPoint(mousePos) - attackPoint.position;
            nextTimeToAttack = Time.time + 1 / currentAttackSpeed;     // Attacks per second
            Shoot();
        }
        currentAttackSpeed = data.AttackSpeed;
    }
}
