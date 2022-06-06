using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class CanvasManager : MonoBehaviour
{
    public UnityEvent<bool> OnPauseMenuChange;

    [Header("Buffs")]
    public RawImage bulletBuff;
    
    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;

    public Button saveButton;
    public Button loadButton;

    public Button backToMenuButton;
    public Button resumeGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("UI")]
    public Text health;
    public Text items;
    

    // Update is called once per frame
    public void activateBulletBuff(bool value)
    {
        bulletBuff.enabled = value;
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame()
    {
        pauseMenu.SetActive(false);
        OnPauseMenuChange.Invoke(false);
        LoadSaveSystem.LoadState();
    }
    public void ResumeGame()
    {
        //  Time.timeScale = 1f;
        //  Cursor.lockState = CursorLockMode.Locked;
         pauseMenu.SetActive(false);
         OnPauseMenuChange.Invoke(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => StartGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (saveButton)
            saveButton.onClick.AddListener(() => GameManager.instance.SaveState());

        if (loadButton)
            loadButton.onClick.AddListener(() => GameManager.instance.LoadState());

        if(resumeGameButton)
            resumeGameButton.onClick.AddListener(() => ResumeGame());
            
        if(backToMenuButton)
            backToMenuButton.onClick.AddListener(() => ReturnToMenu());

        if (GameManager.instance)
            OnPauseMenuChange.AddListener((bool active) => GameManager.instance.PauseMenuChange(active));
            
    }

    void OnLifeValueChange(int value)
    {
        health.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                OnPauseMenuChange.Invoke(pauseMenu.activeSelf);
            }

            
            // if (pauseMenu.activeSelf)
            // {
            //    Time.timeScale = 0f;
            //    Cursor.lockState = CursorLockMode.None;
            // }
            // else
            // {
            //     Time.timeScale = 1f;
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
        }    
        
    }
}
