using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneMenuUIManager : EventListener
{
	public SceneMenuUIManager(Image panel, Image icon) : base(EventSystem.instance)
	{
		menuPanel = panel;
		menuAnim = menuPanel.GetComponent<Animation>();

		menuIcon = icon;
		iconAnim = menuIcon.GetComponent<Animation>();

		clips = new AnimationClip[2];
		clips[0] = Resources.Load("Animations/MenuBarIntro") as AnimationClip;
		clips[1] = Resources.Load("Animations/MenuBarOutro") as AnimationClip;
	}

	private Image menuPanel;
	private Image menuIcon;
	private Animation menuAnim;
	private Animation iconAnim;
	private AnimationClip[] clips;
	private bool active = false;

	private void EnableDisable()
	{
		if (!active)
		{
			menuAnim.clip = clips[0];
			active = true;
		}
		else
		{
			menuAnim.clip = clips[1];
			active = false;
		}

		iconAnim.Play();
		menuAnim.Play();
	}

	public override void handleEvent(Event theEvent)
	{
		if(theEvent.GetEventType() == EventType.SCENE_MENU)
		{
			EnableDisable();
		}
		else if (theEvent.GetEventType() == EventType.SCREEN_PRESSED)
		{
			if (active)
			{
				ScreenPressedEvent spe = theEvent as ScreenPressedEvent;
				if (spe.GetPosition().x <= SceneManager.instance.GetScenePos().x + 6)
				{
					EnableDisable();
				}
			}
		}
	}
}
