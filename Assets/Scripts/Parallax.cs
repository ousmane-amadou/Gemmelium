using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour 
{
	public Transform[] backgrounds;
	public Camera cam;

	private float[] parallax_scales = new float[3];
	private Vector3 camPosition;
	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;
	
		for (int i = 0; i < 3; i++) 
		{
			parallax_scales[i] = (backgrounds[i].position.z * -1)/5f;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < 3; i++) 
		{
			float x = backgrounds[i].position.x + (parallax_scales[i]*(camPosition.x - cam.transform.position.x))/2;
			backgrounds[i].position = new Vector3(x, backgrounds[i].position.y);
		}
		camPosition = cam.transform.position;
		
	}
}
