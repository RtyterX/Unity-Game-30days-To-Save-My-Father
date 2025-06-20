using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool canOpenMenu;

    [Header("Menus")]
    public UnityEngine.GameObject pauseMenu;
    public UnityEngine.GameObject passTime;
    public UnityEngine.GameObject statusMenu;
    public UnityEngine.GameObject map;
    public UnityEngine.GameObject inventoryMenu;
    public UnityEngine.GameObject levelUpMenu;
    public UnityEngine.GameObject deathMenu;

    [Header("Controllers")]
    public InputController input;
    public PlayerMovement2D player;

    public List<UnityEngine.GameObject> menusUIs = new List<UnityEngine.GameObject>();

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
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryMenu.activeSelf == false)
                {
                    CloseOtherMenus(inventoryMenu.name);
                    inventoryMenu.SetActive(true);
                    Time.timeScale = 0f;
                }

            }

        }

    }

    public void CloseOtherMenus(string targetName)
    {
        Debug.Log(targetName);
        foreach (UnityEngine.GameObject obj in menusUIs)
        {
            if (obj.name != targetName)
            {
                obj.SetActive(false);
            }
        }
    }

    // End
}
