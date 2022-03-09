using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveGamepad : PlayerMove
{
    private InputGamepad input;

    protected override void OnEnable() {
        input.Enable();
    }

    protected override void OnDisable() {
        input.Disable();
    }

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        camCinamachine = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        input = PlayerManager.inputGamepad;
    }

    private void Update() {
        movement = input.Player.Movement.ReadValue<Vector2>();
        HandleMovementAnimations();
    }

    private void FixedUpdate() {
        Movement(movement);
    }

    protected override void Movement(Vector2 movement) {
        rb2D.MovePosition(rb2D.position + data.speed * Time.fixedDeltaTime * movement);
    }
}
