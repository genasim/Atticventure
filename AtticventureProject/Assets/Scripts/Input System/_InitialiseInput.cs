using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _InitialiseInput : MonoBehaviour
{
    public static InputSystem playerInput;

    private void Awake() {
        playerInput = new InputSystem();
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }
}
