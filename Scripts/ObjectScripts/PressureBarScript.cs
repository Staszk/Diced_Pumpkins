using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureBarScript : MonoBehaviour {

	public GameObject child;
	private FoodObject parent;
	private PressureButton[] buttons;
	private float speedOfDecay;
	float minGoodPressure = 40f;
	float maxGoodPressure = 60f;
	float currentPressure;

	private void Update()
	{
		int rand = Random.Range(0, 10);

		if (rand <= 3)
		{
			if (currentPressure < 14.7f)
			{
				ChangePressure(speedOfDecay * Time.deltaTime);
			}
			else
			{
				ChangePressure(-speedOfDecay * Time.deltaTime);
			}

			OnPressureChange();
			CheckPressure();
		}
	}

	public void Initialize(FoodObject p, float speed)
	{
		parent = p;
		transform.SetParent(parent.transform);
		speedOfDecay = 10.0f / speed;
		buttons = new PressureButton[2];
		buttons[0] = transform.GetChild(2).GetComponent<PressureButton>();
		buttons[0].Init(this);
		buttons[1] = transform.GetChild(3).GetComponent<PressureButton>();
		buttons[1].Init(this);
		currentPressure = 14.7f;
		OnPressureChange();
	}

	public void ChangePressure(float amount)
	{
		currentPressure += amount;

		if (currentPressure < 0)
		{
			currentPressure = 0;
		}
		else if (currentPressure > 100)
		{
			currentPressure = 100;
		}

		OnPressureChange();
	}

	private void OnPressureChange()
	{
		float pressurePos = ReMap(currentPressure, 0, 100, -0.4625f, 0.4625f);
		child.transform.localPosition = new Vector2(-1.41f, pressurePos);
	}

	private void CheckPressure()
	{
		bool inZone = currentPressure >= minGoodPressure && currentPressure <= maxGoodPressure;

		parent.PressurizeFood(Time.deltaTime, inZone);
	}

	private float ReMap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public void DeleteThis()
	{

		Destroy(gameObject);
	}
}
