using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public ItemObject[] droppingLoot;
    public void DropLoot()
    {
        gameObject.TryGetComponent<HealthController>(out HealthController body);
        foreach (ItemObject item in droppingLoot)
        {
            float dropChance = Random.Range(1, 100);
            Debug.Log("Drop Chance N°:" + dropChance);
            if (dropChance <= item.dropRate)
            {
                Instantiate(item.prefab, transform.position, Quaternion.identity);
                Destroy(gameObject);  // Rework Later
            }
        }
        
    }
}
