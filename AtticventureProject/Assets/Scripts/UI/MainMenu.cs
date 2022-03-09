using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputAction backButton;
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas optionsMenu;

    private EventSystem eventSystem;
    private GameObject firstSelectedMain;
    private GameObject firstSelectedOptions;


    private void OnEnable() {
        backButton.Enable();
        backButton.performed += _ => BackToMainMenu();
    }

    private void OnDisable() {
        backButton.Disable();
        backButton.performed -= _ => BackToMainMenu();
    }

    private void Awake() {
        eventSystem = EventSystem.current;
        firstSelectedMain = GameObject.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject;
        firstSelectedOptions = GameObject.FindObjectOfType<OptionsMenu>().transform.GetChild(0).gameObject;

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
        eventSystem.SetSelectedGameObject(firstSelectedOptions);
    }

    public void BackToMainMenu() {
        optionsMenu.enabled = false;
        mainMenu.enabled = true;
        eventSystem.SetSelectedGameObject(firstSelectedMain);
    }
}
