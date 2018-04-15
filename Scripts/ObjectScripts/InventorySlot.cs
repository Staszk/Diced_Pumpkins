using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
	FoodItem food;

	private void Start()
	{
		ClearSlot();
	}

	public void SetItem(FoodItem _food)
	{
		food = _food;

		icon.sprite = food.icon;
		icon.enabled = true;
		removeButton.interactable = true;
		removeButton.image.enabled = true;
	}

	public void ClearSlot()
	{
		food = null;

		removeButton.interactable = false;
		removeButton.image.enabled = false;
		icon.sprite = null;
		icon.enabled = false;
	}

	public void OnRemoveButton()
	{
		InventoryManager.instance.RemoveFood(food);
	}

	public void OnSlotButton()
	{
		if (food != null)
		{
			if (SceneManager.instance.CanSpawn())
			{

				GameObject foodObject = food.SpawnFood();

				bool worked = SceneManager.instance.SetSceneFood(foodObject.GetComponent<FoodObject>());

				if (worked)
				{
					InventoryManager.instance.RemoveFood(food);
				}
				else
				{
					Destroy(foodObject);
				}
			}
		}
	}
}
