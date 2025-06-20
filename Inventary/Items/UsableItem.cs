using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Usable Items")]
public class UsableItem : InventoryItem
{
    public int numberHeld;
    public UnityEvent useEffect;

    public void useItem(GameObject targetObject)
    {
        useEffect.Invoke();
        numberHeld = -1;

        // Check if Number Held goes 0
        if (numberHeld <= 0)
        {
            PlayerInventory playerInventory = FindAnyObjectByType<PlayerInventory>();
            playerInventory.RemoveUsableItem(name);
        }
    }
}
