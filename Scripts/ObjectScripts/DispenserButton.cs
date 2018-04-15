using UnityEngine;
using System.Collections;

public class DispenserButton : MonoBehaviour
{
	public int dir;
	public FOODDispenser parent;
	public Color[] colors;
	private SpriteRenderer sr;

	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown()
	{
		sr.color = colors[1];
		parent.ChangeIndex(dir);
	}

	private void OnMouseUp()
	{
		sr.color = colors[0];
	}




}
