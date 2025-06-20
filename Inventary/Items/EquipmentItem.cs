using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Equipment")]
public class EquipmentItem : InventoryItem
{
    public bool canEquip;
    public UnityEvent equipEvent;

    public void EquipeThisItem()
    {
        if (canEquip)
        {
            UnityEngine.GameObject player = UnityEngine.GameObject.FindWithTag("Player");
            player.TryGetComponent<PlayerEquipment>(out PlayerEquipment equipController);

          //  equipController.ChangeWeapon(name);
        }
    }
}
