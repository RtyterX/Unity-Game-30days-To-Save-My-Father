using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    // UI
    [Header("UI Assets")]
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;
    public GameObject quitMenu;

    // Options Menu UI
    [Header("Options Menu UI")]
    public GameObject optionsGameplayUI;
    public GameObject optionsControllsUI;
    public GameObject optionsGraficsUI;
    public GameObject optionsAudioUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }


    // --------- Pause Menu Buttoms ----------

    public void Resume()
    {
        GoBackPauseMenu();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ClosePauseMenu()
    {
        pauseMenuUI.SetActive(false);
    }


    // ---- Open Sub Menus ----

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        ClosePauseMenu();
    }

    public void OpenSaveMenu()
    {
        saveMenu.SetActive(true);
        ClosePauseMenu();
    }

    public void OpenLoadMenu() 
    {
        loadMenu.SetActive(true);
        ClosePauseMenu();
    }

    public void OpenQuitMenu()
    {
        quitMenu.SetActive(true);
        ClosePauseMenu();
    }

    // Go Back To Pause Menu And Close 
    public void GoBackPauseMenu()
    {
        if (optionsMenu.active)
        {
            GoBackToOptions();

            optionsMenu.SetActive(false);
        }

        if (saveMenu.active)
        {
            saveMenu.SetActive(false);
        }

        if (loadMenu.active)
        {
            loadMenu.SetActive(false);
        }

        if (quitMenu.active)
        {
            quitMenu.SetActive(false);
        }

        pauseMenuUI.SetActive(true);
    }



    // --- Options Menu Buttons ---

    public void OpenOptionsGraphicsMenu()
    {
        if (optionsAudioUI.active)
        {
            optionsAudioUI.SetActive(false);
        }

        if (optionsControllsUI.active)
        {
            optionsControllsUI.SetActive(false);
        }

        if (optionsGameplayUI.active)
        {
            optionsGameplayUI.SetActive(false);
        }

        optionsGraficsUI.SetActive(true);
    }

    public void OpenOptionsGameplayMenu()
    {
        if (optionsAudioUI.active)
        {
            optionsAudioUI.SetActive(false);
        }

        if (optionsControllsUI.active)
        {
            optionsControllsUI.SetActive(false);
        }

        if (optionsGraficsUI.active)
        {
            optionsGraficsUI.SetActive(false);
        }

        optionsGameplayUI.SetActive(true);
    }

    public void OpenOptionsControllsMenu()
    {
        if (optionsAudioUI.active)
        {
            optionsAudioUI.SetActive(false);
        }

        if (optionsGameplayUI.active)
        {
            optionsGameplayUI.SetActive(false);
        }

        if (optionsGraficsUI.active)
        {
            optionsGraficsUI.SetActive(false);
        }

        optionsControllsUI.SetActive(true);
    }

    public void OpenOptionsAudioMenu()
    {
        if (optionsControllsUI.active)
        {
            optionsControllsUI.SetActive(false);
        }

        if (optionsGameplayUI.active)
        {
            optionsGameplayUI.SetActive(false);
        }

        if (optionsGraficsUI.active)
        {
            optionsGraficsUI.SetActive(false);
        }

        optionsAudioUI.SetActive(true);
    }

    public void GoBackToOptions()
    {
        if (optionsControllsUI.active)
        {
            optionsControllsUI.SetActive(false);
        }

        if (optionsGameplayUI.active)
        {
            optionsGameplayUI.SetActive(false);
        }

        if (optionsGraficsUI.active)
        {
            optionsGraficsUI.SetActive(false);
        }

        if (optionsAudioUI.active)
        {
            optionsAudioUI.SetActive(false);
        }

    }


    // --- Quit Menu Buttons ---

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Carregando o Menu...");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

}

