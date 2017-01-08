using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selection_handler : MonoBehaviour 
{
	public Button[] btn;
	public GameObject WS, TS, MS, nav;

	bool[] selected = { false, false, false, false, false, false };
	int mode_choice;

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}
	public void slct(int id)
	{
		selected [id] = !selected [id];

		if (selected [id] == true) {
			btn[id].image.color = new Color (50f, 50f, 50f, 1f);
		} else if (selected [id] == false) {
			btn[id].image.color = new Color (50f, 50f, 50f, 0.5f);
		}
	}
	public void collapseWelcome(int c)
	{
		mode_choice = c;
		WS.SetActive (false);
		TS.SetActive (true);
		nav.SetActive (true);
	}
	public void collapseTopicSelection(int c)
	{
		TS.SetActive (false);
		MS.SetActive (true);
	}
	public void back()
	{
		if (TS.active == true) {
			TS.SetActive (false);
			WS.SetActive (true);
			nav.SetActive (false);
		} else {
			MS.SetActive (false);
			TS.SetActive (true);
		}
	}
	public void enterClassicMode()
	{
		SceneManager.LoadScene ("Level");
	}
	public void enterMODE()
	{
		int ops = 0;
		if (selected[0] && !selected[1] && !selected[2]) {
			ops = 0;
		} else if (!selected[0] && selected[1] && !selected[2]) {
			ops = 1;
		} else if (!selected[0] && !selected[1] && selected[2]) {
			ops = 2;
		} else if (selected[0] && selected[1] && !selected[2]) {
			ops = 3;
		} else if (selected[0] && !selected[1] && selected[2]) {
			ops = 4;
		} else if (!selected[0] && selected[1] && selected[2]) {
			ops = 5;
		} else {
			ops = 6;
		}
		if (mode_choice == 0) {
			PlayerPrefs.SetInt ("ops", ops);
			SceneManager.LoadScene ("Multiple Choice Of Death");
		} else if (mode_choice == 1) {
			PlayerPrefs.SetInt ("ops", ops);
			SceneManager.LoadScene ("Boot Camp");

		}

	}


}