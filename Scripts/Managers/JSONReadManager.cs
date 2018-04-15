using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class JSONReadManager {

	private Dictionary<string, JsonData> JSONdictionary = new Dictionary<string, JsonData>();

	public static JSONReadManager instance;

	public JSONReadManager()
	{

	}

	public void Initialize()
	{
		if (instance != null)
		{
			return;
		}

		instance = this;
	}

	public void LoadFile(string fileName)
	{
		string jsonString = File.ReadAllText(Application.dataPath + "/JSONFiles/" + fileName + ".json");
		JsonData itemData = JsonMapper.ToObject(jsonString);
		JSONdictionary.Add(fileName, itemData);
	}

	public JsonData GetItemData(string fileName)
	{
		return JSONdictionary[fileName];
	}
	
}
