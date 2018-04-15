/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Structure for storing listeners
 * 		and firing events to be handled
 * 		by the listeners
 */

using UnityEngine;
using System.Collections.Generic;

// Listeners keep track of what EventTypes 
// specific EventListeners care about
public struct Listener {
	public EventType type;
	public EventListener listener;

	public Listener(EventType t, EventListener l)
	{
		type = t;
		listener = l;
	}
}

public class EventSystem {

	public static EventSystem instance;
	private List<Listener> listeners = new List<Listener>();

	public void Initialize(){
		if (instance == null){
			instance = this;
		}
	}

	public void fireEvent(Event theEvent){
		dispatchAllEvents (theEvent);
	}

	public void addListener(EventType type, EventListener listener){
		Listener newElement = new Listener (type, listener);
		listeners.Add (newElement);
	}
		
	private void dispatchAllEvents(Event theEvent){
		foreach (Listener lstnr in listeners){
			if (lstnr.type == theEvent.GetEventType()){
				lstnr.listener.handleEvent (theEvent);
			}
		}
	}
}
