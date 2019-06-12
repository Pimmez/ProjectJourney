using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Text woodText;
	private int totalAmount;

    void Start()
    {
		woodText.text += totalAmount;
    }

	private void AddWoodScore(int amount)
	{
		totalAmount = amount;
		woodText.text = totalAmount.ToString();
	}

	private void OnEnable()
	{
		Inventory.InventoryWoodEvent += AddWoodScore;
	}

	private void OnDisable()
	{
		Inventory.InventoryWoodEvent -= AddWoodScore;
	}
}