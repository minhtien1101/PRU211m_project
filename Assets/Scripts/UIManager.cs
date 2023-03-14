using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject panelGameOver;

	public void ShowPanelGameOver()
	{
		if(panelGameOver)
		{
			panelGameOver.SetActive(true);
		}
	}

	public void ReplayButton()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Scene1");
	}
	public void BackToHome()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Scene0");
	}
	
}
