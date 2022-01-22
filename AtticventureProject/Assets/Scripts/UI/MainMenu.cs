using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputAction backButton;
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas optionsMenu;


    private void OnEnable() {
        backButton.Enable();
        backButton.performed += _ => BackToMainMenu();
    }

    private void OnDisable() {
        backButton.Disable();
        backButton.performed -= _ => BackToMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
        // print("QUIT!!!");
    }
    
    public void OptionsShow() {
        optionsMenu.enabled = true;
        mainMenu.enabled = false;
    }

    public void BackToMainMenu() {
        optionsMenu.enabled = false;
        mainMenu.enabled = true;
    }
}
