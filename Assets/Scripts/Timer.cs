using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public Text time_text;
	public static float seconds;
	
	// Update is called once per frame
	void Update () 
	{
		time_text.text = convert_sec_to_minutes ();
		seconds += Time.deltaTime;
	}

	string convert_sec_to_minutes()
	{
		int minutes = (int) Mathf.Floor (seconds / 60);
		int sec  = ( (int) seconds ) % 60;
		string s = "";

		if (minutes >= 10) { s += minutes + ":"; } 
		else { s+= "0"+ minutes + ":"; }

		if (sec >= 10) { s += sec; } 
		else { s+= "0"+ sec; }

		return s;
	}
}
