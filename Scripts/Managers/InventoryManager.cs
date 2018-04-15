using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager {

	public static InventoryManager instance;

	public InventoryManager()
	{

	}

	public void Initialize()
	{
		if (instance != null)
		{
			Debug.LogWarning("Instance exists");
			return;
		}

		instance = this;
	}

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	private List<FoodItem> foodItems = new List<FoodItem>();
	private int size = 5;

	public bool AddFood(FoodItem food)
	{
		if (foodItems.Count < size)
		{
			foodItems.Add(food);

			if (onItemChangedCallback != null)
			{
				onItemChangedCallback();
			}

			return true;
		}
		return false;
	}

	public void RemoveFood(FoodItem food)
	{
		foodItems.Remove(food);
		onItemChangedCallback();
	}

	public int GetCount()
	{
		return foodItems.Count;
	}

	public int GetInventorySize()
	{
		return size;
	}

	public FoodItem GetItemAtIndex(int index)
	{
		return foodItems[index];
	}

}
