using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	Transform player;

	Vector3 offset = new Vector3 (0f, 0f, -10f);
	float smoothTime = 0.25f;
	Vector3 velocity = Vector3.zero;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = new Vector3(player.position.x, player.position.y, 0) + offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		//transform.position = new Vector3(player.position.x, player.position.y, -10);
	}
}
