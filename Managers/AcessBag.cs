using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcessBag : MonoBehaviour
{
    // UI
    [Header("UI Assets")]
    public GameObject subMenu;

    [Space(10)]
    public GameObject bagMenu;
    public GameObject questsMenu;
    public GameObject mapMenu;
    public GameObject playerStatusMenu;
    public GameObject statisticsMenu;
    public GameObject conquistasMenu;

    // Stop Player
    [Header("Stop Player")]
    public PlayerMovement player;


    void Update()
    {
        // Open Bag Menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (!bagMenu.active)
            {
                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenBagMenu(); 
            }
            else
            {
                CloseAllMenuUI();
            }

        }

        // Open Quests Menu
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!questsMenu.active)
            {
                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenQuestsMenu();

            }
            else
            {
                CloseAllMenuUI();
            }
        }

        // Open Map
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!mapMenu.active)
            {

                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenMap();

            }
            else
            {
                CloseAllMenuUI();
            }
        }

        // Open Player Status
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!playerStatusMenu.active)
            {
                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenPlayerStatusMenu();

            }
            else
            {
                CloseAllMenuUI();
            }
        }

        // Open Statistics Menu
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!statisticsMenu.active)
            {
                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenStatisticsMenu();
            }
            else
            {
                CloseAllMenuUI();
            }
        }

        // Open Achivements Menu
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!conquistasMenu.active)
            {
                CloseMenus();

                if (!subMenu.active)
                {
                    subMenu.SetActive(true);
                }

                OpenConquistasMenu();

            }
            else
            {
                CloseAllMenuUI();
            }
        }



        // Close Menus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllMenuUI();
        }

    }


    // ------------ Buttons ------------

    // Options Menu
    public void OpenBagMenu()
    {
        // Close Others
        CloseMenus();

        bagMenu.SetActive(true);
        player.isPaused = true;

    }


    // Quests Menu
    public void OpenQuestsMenu()
    {
        // Close Others
        CloseMenus();

        questsMenu.SetActive(true);
        player.isPaused = true;

    }


    // Map Menu
    public void OpenMap()
    {
        // Close Others
        CloseMenus();

        mapMenu.SetActive(true);
        player.isPaused = true;

    }


    // Player Status Menu
    public void OpenPlayerStatusMenu()
    {
        // Close Others
        CloseMenus();

        playerStatusMenu.SetActive(true);
        player.isPaused = true;
    }


    // Statistics Menu
    public void OpenStatisticsMenu()
    {
        // Close Others
        CloseMenus();

        statisticsMenu.SetActive(true);
        player.isPaused = true;
    }


    // Conquistas Menu
    public void OpenConquistasMenu()
    {
        // Close Others
        CloseMenus();

        conquistasMenu.SetActive(true);
        player.isPaused = true;
    }

    public void CloseAllMenuUI()
    {
        subMenu.SetActive(false);
        CloseMenus();

        player.isPaused = false;

        Debug.Log("Close All Menus");
    }

    // Close Menu
    public void CloseMenus()
    {

        Debug.Log("Close Menus");

        if (bagMenu.active) 
        {
            bagMenu.SetActive(false);
        }

        if (questsMenu.active)
        {
            questsMenu.SetActive(false);
        }
        
        if (mapMenu.active)
        {
            mapMenu.SetActive(false);
        }
        
        if (playerStatusMenu.active)
        {
            playerStatusMenu.SetActive(false);
        }
        
        if (statisticsMenu.active)
        {
            statisticsMenu.SetActive(false);
        }
        
        if (conquistasMenu.active)
        {
            conquistasMenu.SetActive(false);
        }

        player.isPaused = false;
    }

}
