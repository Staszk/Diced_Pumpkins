/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Base Event class and all 
 * 		inherited event classes
 */

using UnityEngine;

public enum EventType 
{
	INVALID_EVENT_TYPE = -1,
	KEY_PRESSED,
	BOOK_INTERACT,
	PAGE_FLIPPED,
	POINTS_SCORED,
	KNIFE_CUT,
	FOOD_INVENTORY,
	SCENE_MENU,
	SCREEN_PRESSED,
	NUM_EVENT_TYPES 
};
	
public abstract class Event 
{
	public Event (EventType type)
	{
		mType = type;
	}

	private EventType mType;

	public EventType GetEventType() 
	{
		return mType;
	}
}

public class KeyPressedEvent : Event 
{
	public KeyPressedEvent(KeyCode key) : base(EventType.KEY_PRESSED)
	{
		keyPressed = key;
	}

	private readonly KeyCode keyPressed;

	public KeyCode GetKeyCode()
	{
		return keyPressed;
	}
}

public class BookInteractEvent : Event 
{
	public BookInteractEvent() : base(EventType.BOOK_INTERACT)
	{
		// No class data
	}
}

public class PageFlippedEvent : Event 
{
	public PageFlippedEvent() : base(EventType.PAGE_FLIPPED)
	{
		// No class data
	}
}

public class ScorePointsEvent : Event
{
	public enum PointsCategories
	{
		SKILL,
		PRESENTATION
	}

	private readonly PointsCategories category;
	private readonly float score;

	public ScorePointsEvent(PointsCategories _category, float _points) :base(EventType.POINTS_SCORED)
	{
		category = _category;
		score = _points;
	}

	public PointsCategories GetCategory()
	{
		return category;
	}

	public float GetScore()
	{
		return score;
	}
}

public class KnifeCutEvent : Event
{
	private readonly FoodType type;

	public KnifeCutEvent(FoodType _type) : base(EventType.KNIFE_CUT)
	{
		type = _type;
	}

	public FoodType GetCutType()
	{
		return type;
	}
}

public class FoodInventoryEvent : Event
{
	public FoodInventoryEvent() : base(EventType.FOOD_INVENTORY)
	{

	}
}

public class ScreenPressedEvent : Event
{
	private readonly Vector2 position;

	public ScreenPressedEvent(Vector2 _position) : base(EventType.SCREEN_PRESSED)
	{
		position = _position;
	}

	public Vector2 GetPosition()
	{
		return position;
	}
}

public class SceneMenuEvent : Event
{
	public SceneMenuEvent () :base(EventType.SCENE_MENU)
	{

	}
}
