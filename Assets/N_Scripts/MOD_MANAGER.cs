using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MOD_MANAGER : MonoBehaviour 
{
	public GameObject answer_view;
	public GameObject question_view;
	public int choice;

	public Text timer_text;
	public Text question_text;
	public Text[] answer_text;
	public GameObject Result_Panel;
	public GameObject score_prefab;
	public GameObject[] score_panel;
	public Text[] score_panel_score;
	public GameObject game_panel, verdict_panel;
	public Text verdict_panel_text;

	int[] options;
	float time_left;
	int correct_answer; 
	int question_value;
	int question_num;
	float points;



	// Use this for initialization
	void Start () 
	{
		question_num = 0;
		points = 0;
		time_left = 240;
		generateQuestion ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		time_left -= Time.deltaTime;
		string minutes =  ((int) time_left / 60).ToString("D2");
		string seconds = ( (int) time_left % 60).ToString("D2");
		timer_text.text = string.Format ("{0}:{1}", minutes, seconds);

		if (time_left <= 0 || question_num == 15) {
			points += (int) (time_left*0.5);
			closeGame ();
		}

	}

	public void switchView(Text t)
	{ 
		if(t.text == "Q") { 
			answer_view.SetActive (true);
			question_view.SetActive (false);
			t.text = "A"; 
		}
		else {
			answer_view.SetActive (false);
			question_view.SetActive (true);
			t.text = "Q"; 
		}

	}
	public void generateQuestion()
	{
		int[] ops; int size;
		if (PlayerPrefs.GetInt ("ops") == 0) {
			ops = new int[] { 0 }; size = 1;
		} else if (PlayerPrefs.GetInt ("ops") == 1) {
			ops = new int[] { 1 }; size = 1;
		} else if (PlayerPrefs.GetInt ("ops") == 2) {
			ops = new int[] { 2 }; size = 1;
		} else if (PlayerPrefs.GetInt ("ops") == 3) {
			ops = new int[] { 0, 1 }; size = 2;
		} else if (PlayerPrefs.GetInt ("ops") == 4) {
			ops = new int[] { 0, 2 }; size = 2;
		} else if (PlayerPrefs.GetInt ("ops") == 5) {
			ops = new int[] { 1, 2 }; size = 2;
		} else {
			ops = new int[] { 0, 1, 2 }; size = 3;
		}

		int c = Random.Range (0, size);

		switch (ops[c]) {
		case 0:
			dynQ prblm = new dynQ ();
			question_text.text = prblm.question;
			for (int i = 0; i < 4; i++) {
				answer_text [i].text = prblm.answers [i];
			}
			correct_answer = prblm.correct_answer;
			question_value = prblm.question_value;

			break;
		case 1:
			kinQ prblm2 = new kinQ ();
			question_text.text = prblm2.question;
			for (int i = 0; i < 4; i++) {
				answer_text [i].text = prblm2.answers [i];
			}
			correct_answer = prblm2.correct_answer;
			question_value = prblm2.question_value;

			break;
		case 2:
			engQ prblm3 = new engQ ();
			question_text.text = prblm3.question;
			for (int i = 0; i < 4; i++) {
				answer_text [i].text = prblm3.answers [i];
			}
			correct_answer = prblm3.correct_answer;
			question_value = prblm3.question_value;

			break;
		}
	}

	public void submit()
	{
		if (choice == correct_answer) {
			score_panel [question_num].SetActive (true);
			score_panel [question_num].GetComponent<Image> ().color = Color.green;
			score_panel_score [question_num].text = question_value.ToString ();
			points += question_value;
		} else 
		{
			score_panel [question_num].SetActive (true);
			score_panel [question_num].GetComponent<Image> ().color = Color.red;
			score_panel_score [question_num].text = question_value.ToString ();
			points -= question_value*0.01f;
		}

		question_num++;
		generateQuestion (); 

	}
	public void changeChoice(int c)
	{
		choice = c;
	}
	void closeGame()
	{
		game_panel.SetActive (false);
		verdict_panel.SetActive (true);
		verdict_panel_text.text += points.ToString ();

		if (points < 200) {
			verdict_panel_text.text += ". Level 1";
		} else if (points > 150 && points < 250) {
			verdict_panel_text.text += ". Level 2";
		} else if (points > 250 && points < 350) {
			verdict_panel_text.text += ". Level 3";
		} else if (points > 350 && points < 450) {
			verdict_panel_text.text += ". Level 4";
		} else {
			verdict_panel_text.text += ". Level 4+";
		}
	}
	public void loadMenu()
	{
		SceneManager.LoadScene ("Main Menu");
	}
}
