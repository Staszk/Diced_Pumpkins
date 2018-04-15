using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldCanvasManager : EventListener {

	public static WorldCanvasManager instance;

	public void Initialize()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Instance exists");
		}
	}

	public WorldCanvasManager (Canvas _worldCanvas, TextMeshProUGUI _textField, Image img, Sprite[] sprites) :base(EventSystem.instance)
	{
		worldCanvas = _worldCanvas;
		textField = _textField;
		textAnim = textField.GetComponent<Animation>();

		image = img;
		imageAnim = image.GetComponent<Animation>();

		scoreSprites = sprites;

		worldCanvas.enabled = false;
	}

	private readonly Canvas worldCanvas;
	private readonly TextMeshProUGUI textField;
	private readonly Image image;
	private readonly Sprite[] scoreSprites;
	private readonly Animation textAnim;
	private readonly Animation imageAnim;

	private readonly string[] phrases = { "Miss", "Terrible", "Bad", "Meh", "Okay", "Great", "Fantastic", "Perfect!" };

	public override void handleEvent(Event theEvent)
	{
		if (theEvent.GetEventType() == EventType.POINTS_SCORED)
		{
			ScorePointsEvent scoreEvent = theEvent as ScorePointsEvent;

			float score = scoreEvent.GetScore();
			string phrase = "";
			Sprite sprite;

			if (score < 20.0f)
			{
				phrase = phrases[0];
				sprite = scoreSprites[0];
			} 
			else if (score < 30)
			{
				phrase = phrases[1];
				sprite = scoreSprites[0];
			}
			else if (score < 40)
			{
				phrase = phrases[2];
				sprite = scoreSprites[0];
			}
			else if (score < 50)
			{
				phrase = phrases[3];
				sprite = scoreSprites[0];
			}
			else if (score < 65)
			{
				phrase = phrases[4];
				sprite = scoreSprites[1];
			}
			else if (score < 80)
			{
				phrase = phrases[5];
				sprite = scoreSprites[1];
			}
			else if (score < 95)
			{
				phrase = phrases[6];
				sprite = scoreSprites[1];
			}
			else
			{
				phrase = phrases[7];
				sprite = scoreSprites[1];
			}


			ActivateCanvas(phrase, sprite);
		}
	}

	private void ActivateCanvas(string phrase, Sprite sprite)
	{
		textAnim.Stop();
		imageAnim.Stop();

		textField.text = phrase;
		image.sprite = sprite;
		worldCanvas.enabled = true;

		textAnim.Play();
		imageAnim.Play();
	}

	public void MoveCanvas(Vector2 newPos)
	{
		worldCanvas.transform.position = newPos;
	}
}
