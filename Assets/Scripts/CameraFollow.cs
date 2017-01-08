using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public GameObject player;
	public float smoothTime;

	private Transform cam;
	private Vector2 velocity;
	// Use this for initialization
	void Start () 
	{
		cam= transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = Mathf.SmoothDamp( cam.transform.position.x, 
		                           player.transform.position.x + 4, ref velocity.x, smoothTime); 

		float y = Mathf.SmoothDamp( cam.position.y, 
		                         player.transform.position.y + 3, ref velocity.y, smoothTime);

		cam.position = new Vector3 (x, y, cam.position.z);
	}
}



