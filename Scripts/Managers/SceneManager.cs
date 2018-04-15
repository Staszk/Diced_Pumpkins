using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SceneManager {

	public static SceneManager instance;

	public SceneManager(Camera _cam)
	{
		cam = _cam;

		JsonData sceneData = JSONReadManager.instance.GetItemData("SceneObjectFile");

		int elements = (int)sceneData["SceneManager"]["Count"];

		for (int i = 0; i < elements; i++)
		{
			string name = (string)sceneData["SceneManager"]["Scenes"][i]["Name"];
			int ID = (int)sceneData["SceneManager"]["Scenes"][i]["ID"];
			bool canSpawn = (bool)sceneData["SceneManager"]["Scenes"][i]["Can Spawn"];

			float scenePosX = (float)sceneData["SceneManager"]["Scenes"][i]["Scene Pos"]["X"];
			float scenePosY = (float)sceneData["SceneManager"]["Scenes"][i]["Scene Pos"]["Y"];
			Vector2 scenePos = new Vector2(scenePosX, scenePosY);

			float worldUIX = (float)sceneData["SceneManager"]["Scenes"][i]["World UI"]["X"];
			float worldUIY = (float)sceneData["SceneManager"]["Scenes"][i]["World UI"]["Y"];
			Vector2 worldUI = new Vector2(worldUIX, worldUIY);

			float foodSpawnX = (float)sceneData["SceneManager"]["Scenes"][i]["Food Spawn"]["X"];
			float foodSpawnY = (float)sceneData["SceneManager"]["Scenes"][i]["Food Spawn"]["Y"];
			Vector2 foodSpawn = new Vector2(foodSpawnX, foodSpawnY);

			scenes.Add(ID, new Scene(name, ID, canSpawn, scenePos, worldUI, foodSpawn));
		}

		SetScene(0);
	}

	public void Initialize()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Already Instanced");
		}
	}

	Dictionary<int, Scene> scenes = new Dictionary<int, Scene>();
	private Scene currentScene;
	private Camera cam;

	public bool SetSceneFood(FoodObject food)
	{
	
		if (!currentScene.canSpawn || currentScene.currentObject != null)
		{
			return false;
		}

		currentScene.currentObject = food;
		currentScene.currentObject.InitFood();
		currentScene.currentObject.InitFoodState(currentScene.ID);
		return true;
	}

	public bool SetSceneFood(FoodObject food, int index)
	{

		if (!scenes[index].canSpawn || scenes[index].currentObject != null)
		{
			return false;
		}

		scenes[index].currentObject = food;
		scenes[index].currentObject.InitFood();
		scenes[index].currentObject.InitFoodState(index);
		return true;
	}

	public FoodItem RemoveSceneFood(int index)
	{
		if (index == -1)
		{
			index = currentScene.ID;
		}

		if (scenes[index].currentObject == null)
		{
			return null;
		}

		FoodItem temp = scenes[index].currentObject.GetFoodItem();

		scenes[index].currentObject.DestroySelf();
		scenes[index].currentObject = null;

		return temp;
	}

	public bool CanSpawn()
	{
		return currentScene.canSpawn && currentScene.currentObject == null;
	}

	public bool CanRemoveFood()
	{
		if (currentScene.currentObject == null)
		{
			return false;
		}

		return currentScene.currentObject.GetReturnable();
	}

	public Vector2 GetScenePos()
	{
		return currentScene.scenePos;
	}

	public Vector2 GetFoodSpawnPos()
	{
		return currentScene.foodSpawnPos;
	}

	private void SetScene(int ID)
	{
		currentScene = scenes[ID];
		currentScene.InitializeScene(cam);
	}

	public void ChangeScene(int ID)
	{
		if (currentScene.ID == ID)
		{
			return;
		}

		currentScene = scenes[ID];
		currentScene.InitializeScene(cam);
	}
}

public class Scene
{
	public string name;
	public int ID;
	public FoodObject currentObject;
	public bool canSpawn;
	public Vector2 scenePos;
	public Vector2 worldUIPos;
	public Vector2 foodSpawnPos;

	public Scene(string _name, int _ID, bool _canSpawn, Vector2 _scenePos, Vector2 _worldUI, Vector2 _foodSpawnPos)
	{
		name = _name;
		ID = _ID;
		canSpawn = _canSpawn;
		scenePos = _scenePos;
		worldUIPos = _worldUI;
		foodSpawnPos = _foodSpawnPos;
	}

	public void InitializeScene(Camera cam)
	{
		cam.transform.position = new Vector3(scenePos.x, scenePos.y, -10);
		WorldCanvasManager.instance.MoveCanvas(worldUIPos);
	}
}
