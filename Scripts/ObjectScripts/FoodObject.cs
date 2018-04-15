using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public enum FoodType
{
	MEAT = 0,
	VEGETABLE = 1,
	SAUCE = 2,
	SPICE = 3
}

public class FoodObject : MonoBehaviour {
	
	#region General Variables

	public string foodName;
	private bool[] possibleStates = new bool[6];
	private GameObject[] childrenFoods = new GameObject[6];
	private GameObject badResultFood;
	private FoodItem foodItem;
	private FoodType typeOfFood;
	private float passingScore;

	private JsonData foodData;

	private bool returnable = false;

	#endregion

	#region CUTTING STATE VARIABLES /// State: 0

	private List<Vector2> cutSpawnPoints = new List<Vector2>();
	private List<int> cutDirections = new List<int>();
	private List<float> cutVectorLengths = new List<float>();
	private GameObject cutLineObject;
	private int numberOfCuts;
	private int cutsMade = 0;

	#endregion

	#region PLUCKING STATE VARIABLES /// State : 2

	private int numChildrenGameObjects;
	private int numChildrenPlucked = 0;
	private List<Vector2> bowlLocations;
	private GameObject childHolder;
	private GameObject pluckingUIObject;

	#endregion

	#region DE-PRESSURIZER STATE VARIABLES /// State : 3

	private float xPos, yPos;
	private float xScale, yScale;

	PressureBarScript meter;

	private float maxTime;
	private float requiredTime;
	private float speedOfDecay;

	#endregion

	#region General Functions

	public void InitFood()
	{
		foodData = JSONReadManager.instance.GetItemData("FoodObjectFile");

		// Food Type
		int typeInt = (int) foodData[foodName]["Type"];
		typeOfFood = (FoodType)typeInt;

		returnable = (bool)foodData[foodName]["Returnable"];

		// Possible States
		for (int i = 0; i < possibleStates.Length; i++)
		{
			possibleStates[i] = (bool)foodData[foodName]["States"][i];
		}

		// Children Foods
		for (int i = 0; i < childrenFoods.Length; i++)
		{
			string path = (string)foodData[foodName]["Children"][i];
			GameObject childFood = Resources.Load("Prefabs/" + path) as GameObject;

			childrenFoods[i] = childFood;
		}

		string str = (string)foodData[foodName]["Food Item"];
		foodItem = Resources.Load("Food Items/" + str) as FoodItem;

		// Bad Result Food
		string food = (string)foodData[foodName]["Bad Result"];
		badResultFood = Resources.Load("Prefabs/" + food) as GameObject;


		// Passing Score
		passingScore = (float)foodData[foodName]["Pass Score"];
	}

	public FoodItem GetFoodItem()
	{
		return foodItem;
	}

	public bool GetReturnable()
	{
		return returnable;
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	#endregion

	#region CUTTING STATE FUNCTIONS /// State: 0

	public void InitCuttingBoard()
	{
		if (!possibleStates[0])
		{
			return;
		}

		// Cutting Spawn Vectors
		numberOfCuts = (int)foodData[foodName]["Num Cuts"];
		for (int i = 0; i < numberOfCuts; i++)
		{
			float x = (float)foodData[foodName]["Vector Pos"][i][0];
			float y = (float)foodData[foodName]["Vector Pos"][i][1];

			cutSpawnPoints.Add(new Vector2(x, y));
		}

		// Cut Directions
		for (int i = 0; i < numberOfCuts; i++)
		{
			int direction = (int)foodData[foodName]["Cut Dirs"][i];

			cutDirections.Add(direction);
		}

		// Cut Vector Lengths
		for (int i = 0; i < numberOfCuts; i++)
		{
			float length = (float)foodData[foodName]["Vec Length"][i];

			cutVectorLengths.Add(length);
		}

		cutLineObject = Resources.Load("Prefabs/Cut_Line") as GameObject;
		SpawnCutPoints();
	}

	private void SpawnCutPoints()
	{
		for (int i = 0; i < cutSpawnPoints.Count; i++)
		{
			GameObject go = Instantiate(cutLineObject, cutSpawnPoints[i], Quaternion.identity);
			go.transform.SetParent(this.transform);
			CuttingLineScript line = go.GetComponent<CuttingLineScript>();
			line.Initialize(cutDirections[i], cutVectorLengths[i], this, i);
		}
	}

	public void ReloadSpawnPoint(int index)
	{
		GameObject go = Instantiate(cutLineObject, cutSpawnPoints[index], Quaternion.identity);
		CuttingLineScript line = go.GetComponent<CuttingLineScript>();
		line.Initialize(cutDirections[index], cutVectorLengths[index], this, index);
	}

	public void CutSegment(float points)
	{

		if (returnable)
		{
			returnable = false;
		}

		cutsMade++;
		passingScore -= points;

		if (cutsMade == numberOfCuts)
		{
			// Become new food
			if (passingScore <= 0)
			{
				BecomeNewFood(0);
			}
			else
			{
				BecomeNewFood(0, false);
			}
			
			// Delete this food
			Destroy(gameObject);
		}
	}
	#endregion

	#region PLUCKING STATE FUNCTIONS /// State: 2

	private void InitPluckingStation()
	{
		if (!possibleStates[2])
		{
			return;
		}

		numChildrenGameObjects = (int)foodData[foodName]["GO Childs"];
		

		bowlLocations = new List<Vector2>();

		for (int i = 0; i < numChildrenGameObjects; i++)
		{
			float x = (float)foodData[foodName]["BowlVector"][i][0];
			float y = (float)foodData[foodName]["BowlVector"][i][1];

			bowlLocations.Add(new Vector2(x, y));
		}

		pluckingUIObject = Resources.Load("Prefabs/Pluck UI") as GameObject;

		childHolder = new GameObject();

		SpawnPluckablePoint();
	}

	private void SpawnPluckablePoint()
	{
		int x = Random.Range(0, numChildrenGameObjects - numChildrenPlucked);

		GameObject child = transform.GetChild(x).gameObject;
		GameObject pluck = Instantiate(pluckingUIObject, child.transform.position, Quaternion.identity);
		pluck.transform.SetParent(this.transform);
		pluck.GetComponent<PluckableUIScript>().Initialize(this, transform.GetChild(x).gameObject, x);
	}

	public void Pluck(int index)
	{
		if (returnable)
		{
			returnable = false;
		}

		Transform child = transform.GetChild(index);
		child.position = bowlLocations[numChildrenPlucked];
		child.SetParent(childHolder.transform);
		child.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		child.GetComponent<SpriteRenderer>().sortingOrder = 2;

		numChildrenPlucked++;

		if (numChildrenPlucked != numChildrenGameObjects)
		{
			SpawnPluckablePoint();
		}
		else
		{
			BecomeNewFood(2);
			Destroy(childHolder);
			Destroy(gameObject);
		}
	}

	#endregion

	#region DE-PRESSURIZER STATE FUNCTIONS /// State: 3

	private void InitDepressurizer()
	{
		xPos = (float)foodData[foodName]["P. Pos"]["X"];
		yPos = (float)foodData[foodName]["P. Pos"]["Y"];

		xScale = (float)foodData[foodName]["P. Scale"]["X"];
		yScale = (float)foodData[foodName]["P. Scale"]["Y"];

		transform.position = new Vector3(xPos, yPos, 0);
		transform.localScale = new Vector3(xScale, yScale, 1);

		if (!possibleStates[3])
		{
			return;
		}

		maxTime = (float)foodData[foodName]["Max Time"];
		requiredTime = (float)foodData[foodName]["Req. Time"];
		speedOfDecay = (float)foodData[foodName]["Decay Spd"];

		SpawnMeter();
	}

	private void SpawnMeter()
	{
		GameObject meterPrefab = Resources.Load("Prefabs/Pressure Bar") as GameObject;
		GameObject meterObj = Instantiate(meterPrefab, new Vector3(29.1f, -14.5f, 0.0f), Quaternion.identity);
		meter = meterObj.GetComponent<PressureBarScript>();
		meter.Initialize(this, speedOfDecay);
	}

	public void PressurizeFood(float deltaTime, bool inZone)
	{
		maxTime -= deltaTime;

		if (inZone)
		{
			requiredTime -= deltaTime;
		}

		if (maxTime <= 0)
		{
			PressureResult();
		}
	}

	private void PressureResult()
	{
		meter.DeleteThis();

		if (requiredTime <= 0)
		{
			BecomeNewFood(3, true, 3);
		}
		else
		{
			BecomeNewFood(3, false, 3);
		}

		Destroy(gameObject);
	}

	#endregion

	#region FOOD PROPERTIES FUNCTIONS

	private void BecomeNewFood(int index, bool successful = true, int sceneIndex = -1)
	{
		SceneManager.instance.RemoveSceneFood(index);
		FoodObject food;
		GameObject go;

		if (successful)
		{
			go = Instantiate(childrenFoods[index], transform.position, Quaternion.identity);
		}
		else
		{
			go = Instantiate(badResultFood, transform.position, Quaternion.identity);
		}

		food = go.GetComponent<FoodObject>();

		if (sceneIndex != -1)
		{
			SceneManager.instance.SetSceneFood(food, sceneIndex);
		}
		else
		{
			SceneManager.instance.SetSceneFood(food);
		}
		
	}

	public void InitFoodState(int state)
	{
		if (state == 0)
		{
			InitCuttingBoard();
		}
		else if (state == 1)
		{

		}
		else if (state == 2)
		{
			InitPluckingStation();
		}
		else if (state == 3)
		{
			InitDepressurizer();
		}
	}

	public FoodType GetFoodType()
	{
		return typeOfFood;
	}
	#endregion
}
