using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimerEnemy : MonoBehaviour
{
	[SerializeField] GameObject player;
	SpriteRenderer sprt;
	Animator anim;
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
		if (player.transform.position.x < transform.position.x)
		{
			sprt.flipX = false;

		}
		else
		{
			sprt.flipX = true;
		}
		if (Vector3.Distance(player.transform.position, transform.position) <= 1.3f)
		{
			anim.SetInteger("slimerAnim", 1);
		}
		else
		{
			anim.SetInteger("slimerAnim", 0);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BulletPlayer"))
		{
			isGetBulletPlayer = true;
			Debug.Log("Dính đạn của player");
			// destroy bullet player
			Destroy(collision.gameObject);
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
		anim.SetInteger("slimerAnim", 2);
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
