using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertButton : MonoBehaviour {

	public void OnRevertButton()
	{
		if (InventoryManager.instance.GetCount() < InventoryManager.instance.GetInventorySize())
		{
			if (SceneManager.instance.CanRemoveFood())
			{
				FoodItem food = SceneManager.instance.RemoveSceneFood(-1);
				InventoryManager.instance.AddFood(food);
			}
		}
	}
}
