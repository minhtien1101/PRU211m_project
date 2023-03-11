using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEnemy : MonoBehaviour
{

	[SerializeField] Transform bulletPos;
	[SerializeField] GameObject bullet;
	[SerializeField] GameObject player;
	[SerializeField] float timeFire;
	[SerializeField] float xSpeed;
	[SerializeField] float xSpeedBullet;
	[SerializeField] float xStartPoint;
	[SerializeField] float xEndPoint;
	[SerializeField] List<GameObject> listBoxSecretBullet;
	SpriteRenderer sprt;
	Animator anim;
	bool isShooting;
	bool isGetBulletPlayer;
	// Start is called before the first frame update
	void Start()
	{
		sprt = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isGetBulletPlayer) return;
		// saw player
		if (Vector3.Distance(player.transform.position, transform.position) <= 9f)
		{
			anim.SetInteger("dinoAnim", 0);
			if (player.transform.position.x < transform.position.x)
			{
				sprt.flipX = false;
			}
			else
			{
				sprt.flipX = true;
			}
			if (!isShooting)
			{
				// direct left
				if (!sprt.flipX)
				{
					float xPos = bulletPos.localPosition.x > 0 ? bulletPos.localPosition.x * -1 : bulletPos.localPosition.x;
					bulletPos.localPosition = new Vector3(xPos, bulletPos.localPosition.y, bulletPos.localPosition.z);
					GameObject bulletObject = Instantiate(bullet, bulletPos.transform.position, Quaternion.Euler(0, 0, 0));
					bulletObject.GetComponent<Rigidbody2D>().velocity = -1 * bulletObject.transform.right * xSpeedBullet;
					Destroy(bulletObject, 2f);
				}
				// direct right
				else
				{
					float xPos = bulletPos.localPosition.x > 0 ? bulletPos.localPosition.x : bulletPos.localPosition.x * -1;
					bulletPos.localPosition = new Vector3(xPos, bulletPos.localPosition.y, bulletPos.localPosition.z);
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
				sprt.flipX = true;
			}
			else if (xEndPoint <= transform.position.x)
			{
				sprt.flipX = false;
			}
			if (sprt.flipX)
			{
				transform.position += Vector3.right * Time.deltaTime * xSpeed;
			}
			else
			{
				transform.position += Vector3.left * Time.deltaTime * xSpeed;
			}
			anim.SetInteger("dinoAnim", 1);
		}

	}

	IEnumerator TimeToNextFire(float timeFire)
	{
		yield return new WaitForSeconds(timeFire);
		isShooting = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BulletPlayer"))
		{
			Debug.Log("Dính đạn của player");
			// destroy bullet player
			Destroy(collision.gameObject);
			
			isGetBulletPlayer = true;
			// destroy enemy after 0.5s
			StartCoroutine(StartCountDie());
		}
		else if (collision.gameObject.CompareTag("DeathZone"))
		{
			Destroy(gameObject);
		}
	}
	private IEnumerator StartCountDie()
	{
		anim.SetInteger("dinoAnim", 2);
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
		// random box secret
		bool isDropBox = Random.Range(0, 2) == 0 ? false : true;
		if (isDropBox)
		{
			int rand = Random.Range(0, 3);
			GameObject box = Instantiate(listBoxSecretBullet[rand], new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), Quaternion.identity);
			Destroy(box, 5f);
		}
	}
}
