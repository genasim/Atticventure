using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : PlayerMove
{
    private InputKeyboard input;

    protected override void Awake() {
        base.Awake();
        input = PlayerManager.Instance.inputKeyboard;
    }

    protected override void OnEnable() {
        input.Enable();
    }
    
    protected override void OnDisable() {
        input.Disable();
    }

    private void Update() {
        movement = input.Player.Movement.ReadValue<Vector2>();
        HandleMovementAnimations();
        // Debug.Log($"Keyboard: {movement}");
    }

    private void FixedUpdate() {
        Movement(movement);
    }

    protected override void Movement(Vector2 movement) {
        rb2D.MovePosition(rb2D.position + data.Speed * Time.fixedDeltaTime * movement);
    }
}
