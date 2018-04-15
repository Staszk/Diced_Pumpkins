/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Controls the canvas operating
 * 		the instruction book object 
 * 		in the game
 */

using UnityEngine;

public class BookManager : EventListener {

	public BookManager(GameObject _book, Canvas _bookCanvas) : base(EventSystem.instance)
	{
		bookObject = _book;
		book = bookObject.GetComponent<Book>();
		bookCanvas = _bookCanvas;
	}

	private Book book;
	private GameObject bookObject;
	private Canvas bookCanvas;
	private bool bookCanvasActive = false;

	public override void handleEvent (Event theEvent)
	{
		if (theEvent.GetEventType () == EventType.BOOK_INTERACT) 
		{
			bookCanvasActive = !bookCanvasActive;

			if (bookCanvasActive) 
			{
				ActivateBook ();
			} 
			else 
			{
				DeactivateBook ();
			}
		}
	}

	private void ActivateBook ()
	{
		Animation anim = bookObject.GetComponentInParent<Animation> ();
		anim.clip = Resources.Load ("Animations/BookEntranceAnimation") as AnimationClip;
		anim.Play ();
	}

	private void DeactivateBook()
	{
		Animation anim = bookObject.GetComponentInParent<Animation> ();
		anim.clip = Resources.Load ("Animations/BookExitAnimation") as AnimationClip;
		anim.Play ();
	}
}
