using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

	[SerializeField] GameObject player;
	[SerializeField] Transform bulletPos;
	[SerializeField] GameObject bullet;
	[SerializeField] float xSpeed;
	[SerializeField] float xSpeedBullet;
	[SerializeField] float xStartPoint;
	[SerializeField] float xEndPoint;
	[SerializeField] float timeFire;

	bool isShooting;
	bool isDirectLeft = true;
	Animator anim;
	void Start()
	{
		anim = GetComponent<Animator>();
	}


	void Update()
	{
		// saw player
		if (Vector3.Distance(player.transform.position, transform.position) <= 10f)
		{
			anim.SetInteger("botAnim", 0);
			if (player.transform.position.x < transform.position.x)
			{
				FlipLeft();
			}
			else
			{
				FlipRight();
			}
			if (!isShooting)
			{
				// direct left
				if (isDirectLeft)
				{
					GameObject bulletObject = Instantiate(bullet, bulletPos.transform.position, Quaternion.Euler(0, 0, 0));
					bulletObject.GetComponent<Rigidbody2D>().velocity = -1 * bulletObject.transform.right * xSpeedBullet;
					Destroy(bulletObject, 2f);
				}
				// direct right
				else
				{
					GameObject bulletObject = Instantiate(bullet, bulletPos.transform.position, Quaternion.Euler(0, 0, 180));
					bulletObject.GetComponent<Rigidbody2D>().velocity = -1 * bulletObject.transform.right * xSpeedBullet;
					Destroy(bulletObject, 2f);
				}

				isShooting = true;
				StartCoroutine(TimeToNextFire(timeFire));
			}
		}
		// auto run when not see player
		else
		{
			if (xStartPoint >= transform.position.x)
			{
				FlipRight();
			}
			else if (xEndPoint <= transform.position.x)
			{
				FlipLeft();
			}
			if (!isDirectLeft)
			{
				transform.position += Vector3.right * Time.deltaTime * xSpeed;
			}
			else
			{
				transform.position += Vector3.left * Time.deltaTime * xSpeed;
			}
			anim.SetInteger("botAnim", 1);
		}
	}
	IEnumerator TimeToNextFire(float timeFire)
	{
		yield return new WaitForSeconds(timeFire);
		isShooting = false;
	}

	public void FlipLeft()
	{
		isDirectLeft = true;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= Scaler.x > 0 ? 1 : -1;
		transform.localScale = Scaler;
	}
	public void FlipRight()
	{
		isDirectLeft = false;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= Scaler.x > 0 ? -1 : 1;
		transform.localScale = Scaler;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BulletPlayer"))
		{
			Debug.Log("Dính đạn của player");
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("DeathZone"))
		{
			Destroy(gameObject);
		}
	}
}