using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerEquipment: MonoBehaviour
{
    private bool slot1OrNot;
    [Space(5)]
    public GameObject slot1;
    public GameObject slot2;

    [Header("UI")]
    public GameObject slot1UI;
    public GameObject slot2UI;


    public void Start()
    {
        // Test
        ChangeWeapon("HitTest", true);
        ChangeWeapon("Teste1", false);
    }

    public void ChangeWeapon(string targetName, bool slot1OrNot)
    {
        HitDetection[] getAllWeapons = transform.GetComponentsInChildren<HitDetection>(true);

        foreach (HitDetection hit in getAllWeapons)
        {
            if (hit.name == targetName)
            {
                if (slot1OrNot)
                {
                    slot1 = hit.gameObject;
                    AlterSlotImage(slot1.name, true);
                }
                else
                {
                    slot2 = hit.gameObject;
                    AlterSlotImage(slot2.name, false);
                }
            }
        }

    }


    public void AlterSlotImage(string targetName, bool slot1OrNo)
    {
        PlayerInventory inventory = gameObject.GetComponentInChildren<PlayerInventory>();

        foreach (EquipmentItem item in inventory.myEquipmentInventory)
        {
            if (item.name == targetName)
            {
                if (slot1OrNo)
                {
                    Image newImage = slot1UI.GetComponentInChildren<Image>();
                    newImage.sprite = item.itemImage;
                }
                else
                {
                    Image newImage = slot2UI.GetComponentInChildren<Image>();
                    newImage.sprite = item.itemImage;
                }
            }
        }
    }

}
