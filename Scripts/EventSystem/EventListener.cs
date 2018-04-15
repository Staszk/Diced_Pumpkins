/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Base class for Listener objects,
 * 		allowing inherited classes to handle
 * 		events
 */

using UnityEngine;

public abstract class EventListener
{
	public EventListener(EventSystem pEventSystem)
	{
		mpEventSystem = pEventSystem;
	}

	private EventSystem mpEventSystem;

	public abstract void handleEvent (Event theEvent);
}
