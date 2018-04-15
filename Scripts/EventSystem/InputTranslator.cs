/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Translate input from the user 
 * 		into specific game events
 */

using UnityEngine;

public class InputTranslator : EventListener 
{
	public InputTranslator() : base(EventSystem.instance)
	{
		// No class data
	}

	public override void handleEvent (Event theEvent)
	{
		if (theEvent.GetEventType() == EventType.KEY_PRESSED)
		{
			KeyPressedEvent keyEvent = theEvent as KeyPressedEvent;

			switch (keyEvent.GetKeyCode())
			{
			case KeyCode.E: 
				EventSystem.instance.fireEvent (new BookInteractEvent ());
				break;
			}
		}
	}
}
