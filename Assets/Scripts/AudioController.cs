using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

	[SerializeField] AudioSource aus;
	[SerializeField] AudioClip shootingSound;
	[SerializeField] AudioClip gameoverSound;
	[SerializeField] AudioClip winSound;

	[SerializeField]
	[Range(0, 1)]
	float volume = 0.5f;

	public void PlayShootingSound()
	{
		aus.volume = volume;
		aus.PlayOneShot(shootingSound);
	}
	public void PlayWinSound()
	{
		aus.volume = volume;
		aus.PlayOneShot(winSound);
	}
	public void PlayGameoverSound()
	{
		aus.volume = volume;
		aus.PlayOneShot(gameoverSound);
	}
}
