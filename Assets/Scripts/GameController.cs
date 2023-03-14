using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[SerializeField] GameObject menuPanel;
	[SerializeField] GameObject tutorialPanel;
	
	public void StartGame()
	{
		SceneManager.LoadScene("Scene1");
	}

	public void ViewTutorial()
	{
		menuPanel.SetActive(false);
		tutorialPanel.SetActive(true);
	}
	public void CloseTutorial()
	{
		menuPanel.SetActive(true);
		tutorialPanel.SetActive(false);
	}
	public void Quit()
	{
		Application.Quit();
	}

	
}
