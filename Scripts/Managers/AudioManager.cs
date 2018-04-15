/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Manages all audio in the game
 */

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : EventListener {

	public AudioManager() : base(EventSystem.instance)
	{
		audioClipObject = new GameObject ();
		audioClipObject.name = "Audio Clip Object";
		audioClipSource = audioClipObject.gameObject.AddComponent<AudioSource> ();

		audioMusicObject = new GameObject ();
		audioMusicObject.name = "Audio Music Object";
		audioMusicSource = audioMusicObject.gameObject.AddComponent<AudioSource> ();
	}

	private int previousPick = -1;

	private GameObject audioClipObject;
	private AudioSource audioClipSource;
	private GameObject audioMusicObject;
	private AudioSource audioMusicSource;
	private List<AudioClip> pageFlipClips = new List<AudioClip> ();

	public override void handleEvent (Event theEvent){
		if (theEvent.GetEventType() == EventType.BOOK_INTERACT)
		{
			BookOpenCloseSound ();
		}
		else if (theEvent.GetEventType() == EventType.PAGE_FLIPPED)
		{
			FlipPageSound ();
		} 
		else if (theEvent.GetEventType() == EventType.KNIFE_CUT)
		{
			KnifeCutEvent kce = theEvent as KnifeCutEvent;

			if (kce.GetCutType() == FoodType.MEAT)
			{
				audioClipSource.pitch = 1.0f;
				audioClipSource.clip = Resources.Load("AudioClips/knifeCutMeat") as AudioClip;
				audioClipSource.Play();
			}
		}
	}

	public void FlipPageSound()
	{
		int pick;

		do {
			pick = Random.Range (0, pageFlipClips.Count);
		} while (pick == previousPick);

		previousPick = pick;

		audioClipSource.pitch = 1.2f;
		audioClipSource.clip = pageFlipClips [pick];
		audioClipSource.Play ();
	}

	public void Initialize(){
		// Add sounds to list in order to be picked at random later
		pageFlipClips.Add (Resources.Load ("AudioClips/page-flip-01") as AudioClip);
		pageFlipClips.Add (Resources.Load ("AudioClips/page-flip-02") as AudioClip);
		pageFlipClips.Add (Resources.Load ("AudioClips/page-flip-03") as AudioClip);
		pageFlipClips.Add (Resources.Load ("AudioClips/page-flip-04") as AudioClip);
		pageFlipClips.Add (Resources.Load ("AudioClips/page-flip-05") as AudioClip);

		// Starts background music
		audioMusicSource.clip = Resources.Load ("AudioClips/Music/bensound-scifi") as AudioClip;
		audioMusicSource.volume = 0.025f;
		audioMusicSource.loop = true;
		audioMusicSource.Play ();
	}

	private void BookOpenCloseSound(){
		audioClipSource.pitch = 1;
		audioClipSource.clip = Resources.Load("AudioClips/Closing Book Cover Sound Effect") as AudioClip;
		audioClipSource.Play ();
	}
}
