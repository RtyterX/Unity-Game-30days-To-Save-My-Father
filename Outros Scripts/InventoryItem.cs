using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Items")]
public class InventoryItem
{
	public string itemName;
	public string itemId;
	public string itemDescription;
	public Sprite itemImage;
	public int numberHeld;
	public bool usable;
	public bool unique;


}
