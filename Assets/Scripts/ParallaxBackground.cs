using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
	Transform cameraTransform;
	[SerializeField] float currentDis;
	[SerializeField] float limitStartDis;
	[SerializeField] float limitEndDis;
	[SerializeField] float respawnDis;

	public void Start()
	{
		cameraTransform = Camera.main.transform;
	}
	public void FixedUpdate()
	{
		GetDistance();
		Spawning();

	}

	private void Spawning()
	{
		if (limitStartDis < currentDis && currentDis < limitEndDis) return;
		Vector3 pos = transform.position;
		if (currentDis >= limitEndDis)
		{
			pos.x += respawnDis;
		}
		else if (currentDis <= limitStartDis)
		{
			pos.x += -1 * respawnDis;
		}

		transform.position = pos;
	}

	private void GetDistance()
	{
		currentDis = cameraTransform.transform.position.x - transform.localPosition.x;
	}
}
