using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;
    [SerializeField] Canvas optionsMenu;
    [SerializeField] InputAction pauseButton;
    PlayerInput playerInput;

    private static bool gameIsPaused = false;

    private void OnEnable() {
        pauseButton.Enable();
    }

    private void OnDisable() {
        pauseButton.Disable();
    }

    private void Awake() {
        pauseButton.performed += _ => Pause();
    }

    private void Start() {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    private void Pause()
    {
        gameIsPaused = !gameIsPaused;
        if(gameIsPaused) {
            Time.timeScale = 0;
            pauseMenu.enabled = true;
            playerInput.DeactivateInput();
        } else { 
            Time.timeScale = 1;
            pauseMenu.enabled = false;
            optionsMenu.enabled = false;
            playerInput.ActivateInput();
        }
    }

    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        gameIsPaused = false;
    }

    public void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OptionsShow() {
        pauseMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void Quit() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
