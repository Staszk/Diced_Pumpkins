using UnityEngine;
using System.Collections;

public class PressureButton : MonoBehaviour
{
	private PressureBarScript parent;
	public Color[] colors;
	private SpriteRenderer button;
	public int direction;

	public void Init(PressureBarScript p)
	{
		parent = p;

		if (direction == 1)
		{
			button = GameObject.Find("PressureButton_1").GetComponent<SpriteRenderer>();
		}
		else
		{
			button = GameObject.Find("PressureButton_2").GetComponent<SpriteRenderer>();
		}
	}

	private void OnMouseDown()
	{
		button.color = colors[1];
		parent.ChangePressure(3 * direction);
	}

	private void OnMouseUp()
	{
		button.color = colors[0];
	}
}
