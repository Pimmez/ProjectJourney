using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Action<int> InventoryWoodEvent;
	private List<int> woodAmount = new List<int>();
	private int meatAmount;


	
	private void AddToInventory(int amount, int type)
	{
		if(type == 0)
		{
			woodAmount.Add(amount);

			if (InventoryWoodEvent != null)
			{
				InventoryWoodEvent(woodAmount.Count);
			}
		}
		if(type == 1)
		{
			meatAmount += amount;
		}
	}

	private void OnEnable()
	{
		PickUp.PickUpEvent += AddToInventory;
	}

	private void OnDisable()
	{
		PickUp.PickUpEvent -= AddToInventory;
	}
}
