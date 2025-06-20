using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
    {
        [Header("UI Stuff to Change")]
        [SerializeField] private TextMeshProUGUI itemNumberText;
        [SerializeField] private Image itemImage;

        [Header("Variables from Item")]
        public UsableItem thisItem;
        public InventoryManager thisManager;


        public void Setup(UsableItem newItem, InventoryManager newManager)
        {
            thisItem = newItem;
            thisManager = newManager;
            if (thisItem)
            {
                itemImage.sprite = thisItem.itemImage;
                itemNumberText.text = "" + thisItem.numberHeld;
            }
        }


        public void ClickOn()
        {
            if (thisItem)
            {
                thisManager.SetTextAndButton(thisItem.itemDescription, true);
            }
        }
    }

