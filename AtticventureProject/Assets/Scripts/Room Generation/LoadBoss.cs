using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadBoss : MonoBehaviour, IInteractable
{
    private bool interactButton = false;

    InputAction interactKeyboard;
    InputAction interactGamepad;

    private void Awake()
    {
        interactKeyboard = PlayerManager.Instance.inputKeyboard.Player.Interact;
        interactGamepad = PlayerManager.Instance.inputGamepad.Player.Interact;
    }
    private void OnEnable()
    {
        interactKeyboard.performed += _ => interactButton = true;
        interactGamepad.performed += _ => interactButton = true;
        interactKeyboard.canceled += _ => interactButton = false;
        interactGamepad.canceled += _ => interactButton = false;
    }

    private void OnDisable()
    {
        interactKeyboard.performed -= _ => interactButton = true;
        interactGamepad.performed -= _ => interactButton = true;
        interactKeyboard.canceled -= _ => interactButton = false;
        interactGamepad.canceled -= _ => interactButton = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && interactButton) {
            Interact();
        }

    }

    public void Interact() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
