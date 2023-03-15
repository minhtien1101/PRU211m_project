using UnityEngine;
using UnityEngine.SceneManagement;

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
