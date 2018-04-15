using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingLineScript : MonoBehaviour {

	public enum LineDirection
	{
		vertical = 1,
		horizontal,
		diagonal
	};

	private FoodObject food;
	private int index;
	private Vector2 startLoc;
	private Vector2 endLoc;
	private Vector2 lineVector;
	private Vector2 trueVector = new Vector2(0.0f, -4.0f);
	private GameObject child;

	public void Initialize(int direction, float length, FoodObject parent, int index)
	{
		child = transform.GetChild(0).gameObject;
		food = parent;
		this.index = index;

		switch(direction)
		{
			case 0:
				trueVector = new Vector2(0.0f, -length);
				break;
			case 1:
				trueVector = new Vector2(length, 0.0f);
				transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
		}

		child.transform.localScale = new Vector3(child.transform.localScale.x, length * 2.5f, 1);
	}

	private void OnMouseDown()
	{
		startLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		child.GetComponent<Animation>().Play();
	}

	private void OnMouseUp()
	{
		endLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		FadeOut();
		CalculateVector();

		if (Mathf.Abs(lineVector.x) > 1.0f || Mathf.Abs(lineVector.y) > 1.0f)
		{
			float points = ScorePoints(CompareVector());
			food.CutSegment(points);
			EventSystem.instance.fireEvent(new KnifeCutEvent(food.GetFoodType()));
		}
		else
		{
			ReInitThis();
		}
	}

	private void CalculateVector()
	{
		lineVector = endLoc - startLoc;
	}

	private Vector2 CompareVector()
	{
		return lineVector - trueVector;
	}

	private float ScorePoints(Vector2 vec)
	{
		float score = (50 - (50 * Mathf.Abs(vec.x))) + (50 - (50 * Mathf.Abs(vec.y)));
		score = Mathf.Clamp(score, 0, 100);
		EventSystem.instance.fireEvent(new ScorePointsEvent(ScorePointsEvent.PointsCategories.SKILL, score));
		return score;
	}

	private void FadeOut()
	{
		Animation anim = GetComponent<Animation>();
		anim.clip = Resources.Load("Animations/CutLineFadeOut") as AnimationClip;
		anim.Play();
		
	}

	public void ReInitThis()
	{
		food.ReloadSpawnPoint(index);
		Destroy(gameObject);
	}

	public void DestroyThis()
	{
		Destroy(gameObject);
	}
}
