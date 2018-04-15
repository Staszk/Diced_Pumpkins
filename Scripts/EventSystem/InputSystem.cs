/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Every frame, receives any input
 * 		and fires the event associated with
 * 		the input
 */

using UnityEngine;
using System;

public class InputSystem 
{
	public void Update () 
	{
		foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(code))
			{
				EventSystem.instance.fireEvent (new KeyPressedEvent (code));
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D col = Physics2D.OverlapCircle(position, 0.25f);
			
			if (col != null)
			{
				ClickableFood food;
				if (food = col.GetComponent<ClickableFood>())
				{
					food.Interact();
					return;
				}
			}

			EventSystem.instance.fireEvent(new ScreenPressedEvent(position));
		}
	}
}
