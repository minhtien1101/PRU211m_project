using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
	[SerializeField] float timeDelay;
	[SerializeField] string nameScene;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("da va cham next level");
			StartCoroutine(DelayLoadScene());
		}
	}

	IEnumerator DelayLoadScene()
	{
		yield return new WaitForSeconds(timeDelay);
		SceneManager.LoadScene(nameScene);
	}
}
