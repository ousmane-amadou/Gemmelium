using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	private Rigidbody2D myBody;
	//public Animator _anim;
	public float speed = 10;
	public float jump_velocity = 10;

	private float h_input = 0;
	private bool isGrounded = false;
	public Transform groundCheck;

	// Use this for initialization
	void Start ()
	{
		//_anim = GetComponent<Animator> ();
		myBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move (h_input);	
	}

	public void Move(float h_axis)
	{
		myBody.velocity = new Vector2 (speed * h_axis, myBody.velocity.y);
	}

	/**
	 * @param the direction movement.
	 * 		  1 = left, -1 = right, 0 = no movement
	 */
	public void StartMoving(float horizonalInput)
	{
		h_input = horizonalInput;
		if (horizonalInput == 0) 
		{
			//_anim.SetFloat ("direction", 0);
		}
		else if (horizonalInput < 0)
		{
			//_anim.SetFloat ("direction", 2);
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			//_anim.SetFloat ("direction", 2);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void Jump()
	{
		if (myBody.velocity.y == 0) 
		{
			myBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
		}

		/*if (isGrounded) 
		{
			myBody.velocity = new Vector3 (myBody.velocity.x, jump_velocity);
			isGrounded = false;
		}*/
	}
}
