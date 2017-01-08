using UnityEngine;
using System.Collections;
 
public class MovingPlatform : MonoBehaviour 
{
	public float radius;
	public float speed;

	private Rigidbody2D myBody;
	private float[] origin;
	private int direction = 1;
	
	// Use this for initialization
	void Start () 
	{
		myBody = GetComponent<Rigidbody2D> ();
		origin = new float[] { myBody.position.x, myBody.position.y };
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (myBody.position.x >= (origin[0] + radius)) 
		{
			direction = -1;
		} 
		else if (myBody.position.x <= (origin[0] - radius) ) 
		{
			direction = 1;
		}
		Move (direction);
	}

	void Move(float h_axis)
	{
		myBody.velocity = new Vector3 (speed * h_axis, myBody.velocity.y);
	}
}