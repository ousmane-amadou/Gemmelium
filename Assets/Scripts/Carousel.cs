using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Carousel : MonoBehaviour 
{
	public GameObject slide1, slide2;
	// Use this for initialization
	void Start () 
	{
		slide2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void next()
	{
		slide1.SetActive (false);
		slide2.SetActive (true);

	}
	public void back()
	{
		slide1.SetActive (true);
		slide2.SetActive (false);
	}
	public void toMenu()
	{
		SceneManager.LoadScene ("Welcome");
	}


}
