using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

	[SerializeField] float parallaxEffect = 0.5f;
	Transform cameraTransform;
	Vector3 lastCameraPiont;
	// Start is called before the first frame update
	void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraPiont = cameraTransform.position;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 deltaMovement = cameraTransform.position - lastCameraPiont;
		transform.position += deltaMovement * parallaxEffect;
		lastCameraPiont = cameraTransform.position;

		if (Math.Abs(cameraTransform.position.x - transform.position.x) >= 12.8f)
		{
			float offsetPositionX = (cameraTransform.position.x - transform.position.x) % 12.8f;
			transform.position += new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, transform.position.z);
		}
	}
}
