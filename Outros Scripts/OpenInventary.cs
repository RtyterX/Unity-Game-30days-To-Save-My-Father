using System;
using System.Collections;
using UnityEngine;

public class OpenInventary : MonoBehaviour
{

    public GameObject InventoryUI;

    public static bool InventoryOn;

    void Start()
    {
        InventoryOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryOn)
            {
                CloseInventory();
            }
            else
            {
                Inventory();
            }
        }

    }

    public void Inventory()
    {
         // Abre o Menu Inventario
        InventoryUI.SetActive(true);
        InventoryOn = true;

    }

    public void CloseInventory()
    {
         // Fecha o Menu Inventario
        InventoryUI.SetActive(false);
        InventoryOn = false;

    }
}
