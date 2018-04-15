using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : EventListener {

	public InventoryUIManager(Canvas _canvas) :base(EventSystem.instance)
	{
		uiCanvas = _canvas;
		slots = uiCanvas.GetComponentsInChildren<InventorySlot>();
		inventoryPanel = uiCanvas.transform.GetChild(0).gameObject;
		inv = InventoryManager.instance;
		inv.onItemChangedCallback += UpdateUI;

		clips = new AnimationClip[2];
		clips[0] = Resources.Load("Animations/FoodInventoryExpandAnimation") as AnimationClip;
		clips[1] = Resources.Load("Animations/FoodInventoryCloseAnimation") as AnimationClip;
	}

	private InventoryManager inv;
	private Canvas uiCanvas;
	private InventorySlot[] slots;
	private GameObject inventoryPanel;
	private AnimationClip[] clips;
	private bool isOpen;

	public override void handleEvent(Event theEvent)
	{
		if (theEvent.GetEventType() == EventType.FOOD_INVENTORY)
		{
			EnableDisable();
		}
		else if (theEvent.GetEventType() == EventType.SCREEN_PRESSED)
		{
			if (isOpen)
			{
				ScreenPressedEvent spe = theEvent as ScreenPressedEvent;
				if (spe.GetPosition().x >= SceneManager.instance.GetScenePos().x + -1.5 || spe.GetPosition().y >= SceneManager.instance.GetScenePos().y + -0.5f)
				{
					EnableDisable();
				}
			}
		}
	}

	private void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inv.GetCount())
			{
				slots[i].SetItem(inv.GetItemAtIndex(i));
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}

	private void EnableDisable()
	{
		Animation anim = inventoryPanel.GetComponent<Animation>();

		if (!isOpen)
		{
			anim.clip = clips[0];
			isOpen = true;
		} 
		else if (isOpen)
		{
			anim.clip = clips[1];
			isOpen = false;
		}

		anim.Play();
	}
}
