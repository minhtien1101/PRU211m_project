using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
	[SerializeField] float xSpeed;
	[SerializeField] float xMaxPos;
	[SerializeField] float respawnPos;

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x <= xMaxPos)
		{
			transform.position += Vector3.right * respawnPos;
		}
		else
		{
			transform.position += Vector3.left * xSpeed * Time.deltaTime;

		}
	}
}
