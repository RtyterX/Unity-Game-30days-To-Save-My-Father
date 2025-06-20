using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = " Player Inventory")]
public class PlayerInventory : MonoBehaviour
{
    public List<UsableItem> myUsableItensInventory = new List<UsableItem>();
    public List<EquipmentItem> myEquipmentInventory = new List<EquipmentItem>();
    public List<KeyItem> myKeyItensInventory = new List<KeyItem>();


    // ------ Usable Item ------

    public void AddUsableItem(UnityEngine.GameObject addItem)
    {
        // myEquipmentInventory.Add(new UsableItem(addItem));
    }

    public void RemoveUsableItem(string itemName)
    {
        UnityEngine.GameObject player = UnityEngine.GameObject.FindWithTag("Player");
        player.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory);

        foreach (UsableItem usableItem in playerInventory.myUsableItensInventory)
        {
            if (usableItem.name == itemName)
            {
                playerInventory.myUsableItensInventory.Remove(usableItem);
            }
        }
    }
}
