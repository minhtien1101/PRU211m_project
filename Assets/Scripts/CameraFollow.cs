using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	Transform player;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(player.position.x, player.position.y, 0);
	}
}
