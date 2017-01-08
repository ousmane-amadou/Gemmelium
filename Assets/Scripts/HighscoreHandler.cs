using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/** 
 *  <summary>
 *  This Class Handles all the scores, entered in by the user.
 *  All scores are stored locally using unity's Player Prefs System.
 * 
 * 	All the highscore data is modelled by a stack, in which the highest score
 * 	is on the top of the stack and the lowest score is on the bottom:
 * 
 * 	90900 <br>
 *  10022 <br>
 * 	00900 <br>
 * 	00010 <br>
 * 	00001 <br>
 * 
 *  The above data is an example of highscore data. 
 * </summary>
 * 
 * Expectation Coverage: <br>
 * 
 * A1 – Data Types and Expressions <br>
 * Demonstrate the ability to use different data types 
 * and expressions when creating computer programs
 * 
 * A2 – Modular Programming <br>
 * Describe and use modular programming concepts and
 * principles in the creation of computer programs.
 * 
 * A3 – Designing Algorithms <br>
 * design and write algorithms and 
 * subprograms to solve a variety of problems
 * 
 * A4 – Code Maintenance<br>
 * create fully documented program codecreate clear and 
 * maintainable external user -documentation
 * 
 * C1 – Modular Design <br>
 * demonstrate the ability to apply modular design 
 * concepts in computer programs
 * 
 * C2 – Algorithm Analysis <br>
 * analyze algorithms for their
 * effectiveness in solving a problem
 */ 
public class HighscoreHandler : MonoBehaviour 
{
	public GameObject entry_prefab;
	public GameObject content_panel;
	public RectTransform t;

	// A List of all the score data.
	// Should be arranged by highest score to lowest
	private List<string> scores;

	/** 
	 * Precondition: A stack of highscores.
	 * Postcondtion: All the scores laid out on the screen.
	 */
	void Start () 
	{
		scores = new List<string>(PlayerPrefs.GetString ("high_scores").Split ('\n'));

		int c = 0;
		foreach (string s in scores) 
		{
			Debug.Log (s);
			c++;
			AddEntry (s, c);
		}
	}
	/**
	 * @param score the score data for the accessed entry
	 * 
	 * This method takes entry data and creates an entry gameobject
	 * based on the data provided. (score [date, points] and rank)
	 */
	public void AddEntry(string score, int rank)
	{
		string points = score.Split (',') [0];
		string date = score.Split (',') [1];
		Vector3 pos = new Vector3 (0, -25 + ( (rank-1) * -80) );

		GameObject entry = (GameObject) (Instantiate (entry_prefab, pos, Quaternion.identity));
		Text[] entry_texts = entry.GetComponentsInChildren<Text> ();

		// Iterates through every text component in the entry_prefab in order to find
		foreach (Text entry_text in entry_texts) 
		{
			if (entry_text.name == "rank_text") 
			{
				entry_text.text = rank + "";
			} 
			else if (entry_text.name == "date_text") 
			{
				entry_text.text = date;
			}
			else if (entry_text.name == "score_text") 
			{
				entry_text.text = points;
			}
		}

		t.sizeDelta = new Vector2(0, 120*rank);
		entry.GetComponent<Transform> ().SetParent (content_panel.GetComponent<Transform> ());

	}

}