using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOODDispenser : MonoBehaviour {

	public Canvas FOODCanvas;
	public GameObject laser;
	private Transform panels;
	private Transform[] children;
	private int childNum;
	private int currentIndex = 0;
	private bool spawning = false;

	private void Start()
	{
		panels = FOODCanvas.transform.GetChild(1);
		laser.SetActive(false);

		childNum = 0;

		foreach (Transform child in panels.transform)
		{
			childNum++;
		}

		children = new Transform[childNum];

		for (int i = 0; i < childNum; i++)
		{
			children[i] = panels.transform.GetChild(i);
		}

		Init();
	}

	private void Init()
	{
		children[0].gameObject.SetActive(true);

		for (int i = 1; i < childNum; i++)
		{
			children[i].gameObject.SetActive(false);
		}
	}

	public void ChangeIndex(int dir)
	{
		children[currentIndex].gameObject.SetActive(false);

		currentIndex += dir;

		if (currentIndex < 0)
		{
			currentIndex = childNum - 1;
		}
		else if (currentIndex >= childNum)
		{
			currentIndex = 0;
		}

		children[currentIndex].gameObject.SetActive(true);
	}

	public void Spawn(float time, GameObject food, Vector2 pos, Vector2 scale)
	{
		if (!spawning)
		{
			StartCoroutine(SpawnFood(time, food, pos, scale));
			spawning = true;
		}
	}

	IEnumerator SpawnFood(float time, GameObject clickableFood, Vector2 pos, Vector2 scale)
	{
		laser.SetActive(true);
		yield return new WaitForSeconds(time);

		GameObject fd = Instantiate(clickableFood, pos, Quaternion.identity);
		fd.transform.localScale = new Vector3(scale.x, scale.y, 1);

		spawning = false;
		laser.SetActive(false);
	}
}
