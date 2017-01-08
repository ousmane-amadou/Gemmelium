using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * <summary>
 * Every sequence will need to have a sequence listener. This listener 
 * allows the sequence gui to respond to number pad, enter, clear
 * and value inputs.
 * 
 * Expectation Coverage: <br>
 * 
 * A1 – Data Types and Expressions <br>
 * Demonstrate the ability to use different data types 
 * and expressions when creating computer programs
 * 
 * A4 – Code Maintenance <br>
 * create fully documented program code create clear and 
 * maintainable external user -documentation
 * 
 * C1 – Modular Design <br>
 * demonstrate the ability to apply modular design 
 * concepts in computer programs
 */ 
public class SequenceListener : MonoBehaviour
{
	public Button selected_input;
	public bool minimized;
	public GameObject[] minimizable_windows; 
	
	public void onNumPadInput(Button b) 
	{
		string value = GameObject.Find (b.name + "/Text").GetComponent<Text> ().text;
		Text selected_input_text = selected_input.GetComponentInChildren<Text> ();

		if (selected_input_text.text.Contains("I") || selected_input_text.text.Contains("A"))
		{
			selected_input_text.text = value;
		} 
		else 
		{
			selected_input_text.text += value;
		}
	}

	public void onChangeInput(Button input)
	{
		selected_input = input;
	}
	public void onBackSpace()
	{
		Text selected_input_text = selected_input.GetComponentInChildren<Text> ();

		string penis = selected_input_text.text;
		selected_input_text.text = penis.Substring (0, penis.Length - 1);
	}
	public void onClear()
	{
		Text selected_input_text = selected_input.GetComponentInChildren<Text> ();
		selected_input_text.text = "";
	}
	public void onShowQuestion(GameObject q)
	{
		q.SetActive (true);
	}
	public void onMinimize(GameObject b)
	{
		b.SetActive (false);
	}
}
