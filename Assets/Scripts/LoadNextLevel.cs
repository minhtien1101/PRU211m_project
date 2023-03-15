using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
	[SerializeField] float timeDelay;
	[SerializeField] string nameScene;
	[SerializeField] GameObject winTitle;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioController audio = FindObjectOfType<AudioController>();
		if (collision.gameObject.CompareTag("Player") && winTitle == null)
		{
			StartCoroutine(DelayLoadScene());
		}
		else if (collision.gameObject.CompareTag("Player") && winTitle != null)
		{
			winTitle.SetActive(true);
			audio.PlayWinSound();
			Invoke("BackToMenu", timeDelay);
		}
	}

	IEnumerator DelayLoadScene()
	{
		yield return new WaitForSeconds(timeDelay);
		SceneManager.LoadScene(nameScene);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(nameScene);
	}
}
