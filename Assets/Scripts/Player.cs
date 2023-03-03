using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	float xSpeed;
	[SerializeField]
	float jump;

	Rigidbody2D rigid;
	SpriteRenderer sprite;
	Animator animat;
	bool isJump;
	// Start is called before the first frame update
	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		animat = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * xSpeed * Time.deltaTime;
			sprite.flipX = false;
			animat.SetInteger("playerAni", 1);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			sprite.flipX = true;
			transform.position += Vector3.left * xSpeed * Time.deltaTime;
			animat.SetInteger("playerAni", 1);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!isJump)
			{
				rigid.AddForce(Vector2.up * jump);
				isJump = true;
				animat.SetInteger("playerAni", 2);
			}
		}
		if(isJump)
		{
			animat.SetInteger("playerAni", 2);
		}
		if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.Space))
		{
			animat.SetInteger("playerAni", 0);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isJump = false;
		}
	}
}
