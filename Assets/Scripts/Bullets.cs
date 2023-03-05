using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	float xSpeed;
	// Start is called before the first frame update
	void Start()
	{
		int xDirection = 0;
		if (transform.rotation.x == 90)
		{
			xDirection = 1;

		}
		else
		{
			xDirection = -1;
		}
		rb.velocity = transform.up * xSpeed * xDirection;
		Destroy(gameObject, 2f);
	}
}
