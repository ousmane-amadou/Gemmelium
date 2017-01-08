using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/** <summary> 
 * The Falling Sequence
 * 
 * 
 */  
public class FallingSequence : MonoBehaviour, Sequence
{
	public Problem prblm;
	public Transform sequence_env;
	
	private GameObject height_input, time_input, question_panel_text;
	private GameObject main_gui;
	
	// Use this for initialization
	void Start () 
	{
		initFieldAccess ();
		generateProblem ();
		displayQuestion ();
		initEnvironment ();
	}
	public void initEnvironment ()
	{
		Instantiate (sequence_env.gameObject, Camera.main.transform.position, Quaternion.identity);
		GameObject.Find ("FALL_ENV(Clone)").transform.SetParent (Camera.main.transform);
	}

	public void initFieldAccess()
	{
		main_gui = GameObject.Find ("GUI"); main_gui.SetActive (false);
		time_input = GameObject.Find ("TIME_INPUT/numpad_inputfield/Text");
		question_panel_text = GameObject.Find ("Question Panel/Text");
	}

	public void generateProblem()
	{
		float[] givens = new float[2];
		givens[0] = Random.Range(5, 120) * 1f; // HEIGHT OF FALL
		
		string question = 
			"A rock is dropped from a height of " + givens [0] + " m.\n"
		  + "Calculate the time it will take for the rock to hit the ground";
		
		//CALCULATE ANSWERS
		float[] answers = calculateAnswers (givens);
		prblm = new Problem (question, givens, answers);
		
		Debug.Log ("Time of fall = " + answers[0]);
	}

	/**
	 * <summary>
	 * <h1> CALCULATIONS!! </h1>
	 * Step 1: Find time of fall <br>
	 * d = vi • t + ½ • a • t^2 <br>
	 * d = 0 + ½ • a • t^2 <br>
	 * t = SQRT(2d/a)
	 * </summary>
	*/ 
	public float[] calculateAnswers(float[] givens)
	{
		float time_of_fall = Mathf.Sqrt(2f*givens[0]/9.8f);
		return new float[] { time_of_fall };
	}

	public void displayQuestion()
	{
		question_panel_text.GetComponent<Text> ().text = prblm.getQuestion ();
	}

	public void onEnter()
	{
		float time_submission = float.Parse(time_input.GetComponent<Text>().text);
		bool correct = checkSubmission (time_submission);
		
		if (correct) 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(-30);
			CharacterHealth.inSequence = false;
			
			Game_Loop.points += 500;
			Game_Loop.problem_in_sequence = false;
			Game_Loop.finished_problems++;

			
			StartCoroutine(endSequence());
		} 
		else 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(30);
			time_input.GetComponent<Text> ().text = "TIME";
		}
	}
	/** 
	 * 1. Removes the invisible object preventing the boulder from falling.
	 * 2. Allows environment to act for 2 seconds
	 * 3. Reactives movement gui's and Destroy's Sequence Objects
	 */ 
	public IEnumerator endSequence()
	{
		GameObject.Find ("Holder").transform.Translate (new Vector3 (10, 0, 0));
		yield return new WaitForSeconds (2);

		Destroy (GameObject.Find ("FALL_ENV(Clone)")); 
		main_gui.SetActive (true);
		Destroy (this.gameObject);
	}

	bool checkSubmission(float time)
	{
		bool time_correct = prblm.getAnswers () [0] >= time - 1
			&& prblm.getAnswers () [0] <= time + 1; 
		
		if (time_correct)
		{
			return true;
		}
		return false;
	}
}