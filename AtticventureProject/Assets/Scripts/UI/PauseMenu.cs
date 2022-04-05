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
        eventSystem = EventSystem.current;
        firstSelectedPause = GameObject.FindObjectOfType<PauseMenu>().transform.GetChild(0).gameObject;
        firstSelectedOptions = GameObject.FindObjectOfType<OptionsMenu>().transform.GetChild(0).gameObject;
    }

    private void Pause()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused) {
            Time.timeScale = 0;
            pauseMenu.enabled = true;
            eventSystem.SetSelectedGameObject(firstSelectedPause);
            PlayerManager.Instance.inputGamepad.Disable();
            PlayerManager.Instance.inputKeyboard.Disable();
        }
        else { 
            Time.timeScale = 1;
            pauseMenu.enabled = false;
            optionsMenu.enabled = false;
            eventSystem.SetSelectedGameObject(null);
            PlayerManager.Instance.inputGamepad.Enable();
            PlayerManager.Instance.inputKeyboard.Enable();
        }
    }

    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        gameIsPaused = false;
        PlayerManager.Instance.inputGamepad.Enable();
        PlayerManager.Instance.inputKeyboard.Enable();
    }

    public void SceneReset()
    {
        pauseMenu.enabled = false;
        optionsMenu.enabled = false;
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
