using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Food/FoodItem")]
public class FoodItem : ScriptableObject {

	new public string name = "New Food";
	public Sprite icon = null;
	public GameObject spawnableFood = null;

	public GameObject SpawnFood()
	{
		GameObject go = Instantiate(spawnableFood, SceneManager.instance.GetFoodSpawnPos(), Quaternion.identity);
		FoodObject fs = go.GetComponent<FoodObject>();
		fs.InitFood();

		return go;
	}
}
