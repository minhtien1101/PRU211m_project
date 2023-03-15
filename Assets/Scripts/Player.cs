using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField]
	float xSpeed;
	[SerializeField]
	float jump;
	[SerializeField]
	float timeSpawn;
	[SerializeField]
	Transform bulletPositionSpawn;
	[SerializeField]
	GameObject bulletObject;
	[SerializeField]
	List<GameObject> listBullet;

	int indexBulletObject = 0;
	Rigidbody2D rigid;
	SpriteRenderer sprite;
	Animator animat;
	bool isJump;
	bool isShooting;
	AudioController ausController;
	UIManager uiManager;
	float button = 0;
	bool JumpButtonDown = false;
	bool buttonFireDown = false;
	// Start is called before the first frame update
	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		animat = GetComponent<Animator>();
		ausController = FindObjectOfType<AudioController>();
		uiManager = FindObjectOfType<UIManager>();

		if (SceneManager.GetActiveScene().name != "Scene1")
		{
			indexBulletObject = PlayerPrefs.GetInt("indexBulletObject");
		}
		bulletObject = listBullet[indexBulletObject];
		PlayerPrefs.SetInt("indexBulletObject", indexBulletObject);
	}

	// Update is called once per frame
	void Update()
	{
		// left = -1, right = 1, default = 0
		float xDirection = Input.GetAxisRaw("Horizontal") ==0? button: Input.GetAxisRaw("Horizontal");
		// move left, right
		if (xDirection < 0 || xDirection > 0)
		{
			if (!isJump)
			{
				animat.SetInteger("playerAni", 1);
			}
			if (xDirection < 0)
			{
				sprite.flipX = true;
				float xPos = bulletPositionSpawn.localPosition.x > 0 ? bulletPositionSpawn.localPosition.x * -1 : bulletPositionSpawn.localPosition.x;
				bulletPositionSpawn.localPosition = new Vector3(xPos, bulletPositionSpawn.localPosition.y, bulletPositionSpawn.localPosition.z);
			}
			else
			{
				sprite.flipX = false;
				float xPos = bulletPositionSpawn.localPosition.x > 0 ? bulletPositionSpawn.localPosition.x : bulletPositionSpawn.localPosition.x * -1;
				bulletPositionSpawn.localPosition = new Vector3(xPos, bulletPositionSpawn.localPosition.y, bulletPositionSpawn.localPosition.z);
			}
			transform.position += Vector3.right * xDirection * xSpeed * Time.deltaTime;

		}
		// jump
		if ((Input.GetKeyDown(KeyCode.Space) || JumpButtonDown) && !isJump)
		{
			rigid.AddForce(Vector2.up * jump);
			isJump = true;
		}
		if (isJump && xDirection != 0)
		{
			animat.SetInteger("playerAni", 2);
		}
		if (xDirection == 0 && !isJump)
		{
			animat.SetInteger("playerAni", 0);
		}
		// shooting
		if ((Input.GetKeyDown(KeyCode.R) || buttonFireDown) && !isShooting)
		{
			ausController.PlayShootingSound();
			isShooting = true;
			buttonFireDown = false;
			if (sprite.flipX)
			{
				Instantiate(bulletObject, bulletPositionSpawn.position, Quaternion.Euler(new Vector3(0, 0, -90)));
			}
			else
			{
				Instantiate(bulletObject, bulletPositionSpawn.position, Quaternion.Euler(new Vector3(0, 0, 90)));
			}
			Debug.Log("Ban da ban");
			StartCoroutine(ShootingCountDown(timeSpawn));
		}

	}

	IEnumerator ShootingCountDown(float time)
	{
		yield return new WaitForSeconds(time);
		isShooting = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isJump = false;
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{
			ausController.PlayGameoverSound();
			uiManager.ShowPanelGameOver();
			Time.timeScale = 0;
		}
		else if (collision.gameObject.CompareTag("Box1"))
		{
			indexBulletObject = 0;
			bulletObject = listBullet[indexBulletObject];
			PlayerPrefs.SetInt("indexBulletObject", indexBulletObject);
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Box2"))
		{
			indexBulletObject = 1;
			bulletObject = listBullet[indexBulletObject];
			PlayerPrefs.SetInt("indexBulletObject", indexBulletObject);
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Box3"))
		{
			indexBulletObject = 2;
			bulletObject = listBullet[indexBulletObject];
			PlayerPrefs.SetInt("indexBulletObject", indexBulletObject);
			Destroy(collision.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BulletEnemy"))
		{
			Destroy(collision.gameObject);
			ausController.PlayGameoverSound();
			uiManager.ShowPanelGameOver();
			Time.timeScale = 0;
		}
		else if (collision.gameObject.CompareTag("DeathZone"))
		{
			ausController.PlayGameoverSound();
			uiManager.ShowPanelGameOver();
			Time.timeScale = 0;
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{
			ausController.PlayGameoverSound();
			uiManager.ShowPanelGameOver();
			Time.timeScale = 0;
		}
	}


	public void ButtonRightDown()
	{
		button = 1;
	}
	public void ButtonRightUp()
	{
		button = 0;
	}

	public void ButtonLeftDown()
	{
		button = -1;
	}
	public void ButtonLeftUp()
	{
		button = 0;
	}

	public void ButtonJumpDown()
	{
		JumpButtonDown = true;
	}
	public void ButtonJumpUp()
	{
		JumpButtonDown = false;
	}
	public void ButtonFireDown()
	{
		isShooting = false;
		buttonFireDown = true;
	}
}
