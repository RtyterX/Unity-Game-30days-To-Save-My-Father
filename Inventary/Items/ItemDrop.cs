using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public InventoryItem drop;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindWithTag("Player");
            PlayerInventory playerInventory = player.GetComponentInChildren<PlayerInventory>();

            if (drop is UsableItem)
            {
                playerInventory.myUsableItensInventory.Add((UsableItem)drop);
            }
            if (drop is EquipmentItem)
            {
                playerInventory.myEquipmentInventory.Add(drop as EquipmentItem);
            }
            if (drop is KeyItem)
            {
                playerInventory.myKeyItensInventory.Add((KeyItem)drop);
            }

            Destroy(gameObject);
        }

    }
}
