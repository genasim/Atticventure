using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private Canvas optionsMenu;
    [SerializeField] private InputAction pauseButton;
    private InputSystem playerInput;
    // PlayerInput playerInput;
    private static bool gameIsPaused = false;

    private EventSystem eventSystem;
    private GameObject firstSelectedPause;
    private GameObject firstSelectedOptions;

    private void OnEnable() {
        pauseButton.Enable();
        pauseButton.performed += _ => Pause();
    }

    private void OnDisable() {
        pauseButton.Disable();
        pauseButton.performed -= _ => Pause();
    }

    private void Awake() {
        playerInput = _InitialiseInput.playerInput;
        eventSystem = EventSystem.current;
        firstSelectedPause = GameObject.FindObjectOfType<PauseMenu>().transform.GetChild(0).gameObject;
        firstSelectedOptions = GameObject.FindObjectOfType<OptionsMenu>().transform.GetChild(0).gameObject;
        // playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    private void Pause()
    {
        gameIsPaused = !gameIsPaused;
        if(gameIsPaused) {
            Time.timeScale = 0;
            pauseMenu.enabled = true;
            eventSystem.SetSelectedGameObject(firstSelectedPause);
            // playerInput.DeactivateInput();
            playerInput.Disable();
        } else { 
            Time.timeScale = 1;
            eventSystem.SetSelectedGameObject(null);
            pauseMenu.enabled = false;
            optionsMenu.enabled = false;
            // playerInput.ActivateInput();
            playerInput.Enable();
        }
    }

    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        gameIsPaused = false;
    }

    public void SceneReset()
    {
        pauseMenu.enabled = false;
        optionsMenu.enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
    }

    public void OptionsShow() {
        pauseMenu.enabled = false;
        optionsMenu.enabled = true;
        eventSystem.SetSelectedGameObject(firstSelectedOptions);
    }
    public void BackToPauseMenu() {
        optionsMenu.enabled = false;
        pauseMenu.enabled = true;
        eventSystem.SetSelectedGameObject(firstSelectedPause);
    }

    public void Quit() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
