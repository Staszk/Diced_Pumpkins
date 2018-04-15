using UnityEngine;
using System.Collections;

public class PluckableUIScript : MonoBehaviour
{
	private FoodObject parent;
	int index;
	private float minDistance = 0.75f;
	private Vector2 startVec;
	private Vector2 endVec;
	private bool plucking = false;
	private GameObject plucked;


	public void Initialize(FoodObject p, GameObject pluckable, int i)
	{
		plucked = pluckable;
		parent = p;
		index = i;
	}

	private void OnMouseDown()
	{
		startVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		plucking = true;
	}

	private void OnMouseUp()
	{
		plucking = false;
		endVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float distance = Vector2.Distance(startVec, endVec);

		if (Mathf.Abs(distance) >= minDistance)
		{
			parent.GetComponent<FoodObject>().Pluck(index);
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (plucking)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(pos.x, pos.y, 0);
			plucked.transform.position = new Vector3(pos.x, pos.y, 0);
		}
	}
}
