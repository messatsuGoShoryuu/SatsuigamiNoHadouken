using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public void OnStart()
	{
		GameState.Init();
		GameState.Reset();
		SceneManager.LoadScene("Gameplay");
	}
}
