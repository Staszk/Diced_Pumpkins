using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
	public int sceneID;

	public void SwitchScene()
	{
		SceneManager.instance.ChangeScene(sceneID);
	}
}
