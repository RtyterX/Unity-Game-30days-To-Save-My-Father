using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    // UI
    [Header("UI Assets")]
    public UnityEngine.GameObject pauseMenuUI;
    public UnityEngine.GameObject optionsMenu;
    public UnityEngine.GameObject saveMenu;
    public UnityEngine.GameObject loadMenu;
    public UnityEngine.GameObject quitMenu;


    // Options Menu UI
    [Header("Options Menu UI")]
    public UnityEngine.GameObject optionsGameplayUI;
    public UnityEngine.GameObject optionsControllsUI;
    public UnityEngine.GameObject optionsGraficsUI;
    public UnityEngine.GameObject optionsAudioUI;


//    void Update()
   // {
      //  if (Input.GetKeyDown(KeyCode.Escape))
     //   {
           // if (pauseMenuUI)
      //    {
         //       Resume();
       //     }
       //     else
      //      {
       //         OpenPauseMenu();
       //     }
     //   }
//
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
   //         if (inventoryMenu)
   //         {
    //            Resume();
    //        }
   //         else
   //         {
   ////             OpenInventoryMenu();
    //        }
   //     }

  //  }


    // --------- Pause Menu Buttoms ----------


    // ---- Open Sub Menus ----


    // Go Back To Pause Menu And Close 



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

