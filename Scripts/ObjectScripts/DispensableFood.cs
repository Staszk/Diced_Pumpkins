using UnityEngine;
using System.Collections;

public class DispensableFood : MonoBehaviour
{
	public GameObject dispensableFood;
	private FOODDispenser parent;
	public Vector2 pos;
	public Vector2 scale;
	public float time;

	private void Start()
	{
		parent = GameObject.FindGameObjectWithTag("Dispenser").GetComponent<FOODDispenser>();
	}

	public void OnClick()
	{
		parent.Spawn(time, dispensableFood, pos, scale);
	}
}
