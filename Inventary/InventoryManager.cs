using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private UnityEngine.GameObject blankInventorySlot;
    [SerializeField] private UnityEngine.GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private UnityEngine.GameObject useButton;

    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if(playerInventory)
        {
            //for(int i = 0; playerInventory.myInventory.Count; i++)
         //   {
           //     InventorySlot newSlot = Instantiate(blankInventorySlot, transform.position, transform.rotation)
           //         .GetComponent<InventorySlot>();
          //      newSlot.transform.SetParent(inventoryPanel.transform);
           //     newSlot.Setup(playerInventory.myInventory[i], this);
          //  }
        }
    }


    // ----- Usable Itens -----

    public void UseItem(UsableItem thisItem)
    {

    }


    // ----- Equipments -----

    public void EquipItem(EquipmentItem thisItem, bool slop1OrNot)
    {
        UnityEngine.GameObject player = UnityEngine.GameObject.FindWithTag("Player");
        player.TryGetComponent<PlayerEquipment>(out PlayerEquipment equipController);
        
        if (slop1OrNot)
        {
            equipController.ChangeWeapon(thisItem.name, slop1OrNot);
            Debug.Log("Equipmente 1 was Equiped");
        }
        else
        {
            equipController.ChangeWeapon(thisItem.name, !slop1OrNot);
            Debug.Log("Equipmente 2 was Equiped");
        }

    }


}
