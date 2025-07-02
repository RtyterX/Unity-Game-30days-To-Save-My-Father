using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool canOpenMenu;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject passTime;
    public GameObject statusMenu;
    public GameObject map;
    public GameObject inventoryMenu;
    public GameObject levelUpMenu;
    public GameObject deathMenu;

    [Header("Controllers")]
    public InputController input;
    public PlayerMovement2D player;

    public List<GameObject> menusUIs = new List<GameObject>();

    void Start()
    {
        // List
        menusUIs.Add(pauseMenu);
        menusUIs.Add(passTime);
        menusUIs.Add(statusMenu);
        menusUIs.Add(map);
        menusUIs.Add(inventoryMenu);
        menusUIs.Add(levelUpMenu);
        menusUIs.Add(deathMenu);

        // Start with Menus Closed
        CloseOtherMenus("");
    }

    void Update()
    {
        if (canOpenMenu)
        {

          //  if (pauseMenu && passTime && statusMenu && map && deathMenu )
          //  {
               // player.isPaused = true;

         //   }

            // Pause Menu
          //  if (input.Pause != 0 && !deathMenu.active)
          //  {


         //   }


            // Inventory
           // if (Input.GetKeyDown(KeyCode.I))
           // {
              //  if (inventoryMenu.activeSelf == false)
              //  {
                   // CloseOtherMenus(inventoryMenu.name);
                  //  inventoryMenu.SetActive(true);
                  //  Time.timeScale = 0f;
            //    }

         //   }

        }

    }

    public void CloseOtherMenus(string targetName)
    {
        Debug.Log(targetName);
        foreach (GameObject obj in menusUIs)
        {
            if (obj.name != targetName)
            {
                obj.SetActive(false);
            }
        }
    }

    // End
}
