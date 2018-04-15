using UnityEngine;
using System.Collections;

public class ClickableFood : MonoBehaviour
{
	public FoodItem foodItem;

	public void Interact()
	{
		PickUp();
	}

	private void PickUp()
	{
		bool added = InventoryManager.instance.AddFood(foodItem);

		if (added)
		{
			Destroy(gameObject);
		}
	}
}
