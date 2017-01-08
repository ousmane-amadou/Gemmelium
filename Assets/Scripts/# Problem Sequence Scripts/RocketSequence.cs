using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketSequence : MonoBehaviour, Sequence
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
		GameObject.Find ("LAUNCH_ENV(Clone)").transform.SetParent (Camera.main.transform);
	}
	
	public void generateProblem()
	{
		float[] givens = new float[2];
		givens[0] = Random.Range(5, 120) * 1f; // INITIAL VELOCITY
		givens [1] = Random.Range(1, 10) * 1f;  // ACCELERATION
		
		string question = 
			"A rocket is fired vertically upwards with initial velocity of " +
			 givens[0] + " m/s at the ground level. \n" +
			"\n Its engines then fire and it is accelerated at " + givens[1] 
			 + " m/s^2 " + "until it reaches an altitude of 1000 m. " +
			 "At that point the engines fail and the rocket goes into free-fall. " +
			 "\nDisregard air resistance. How long was the rocket above the ground?  " +
			 "What is the maximum altitude? ";
		
		//CALCULATE ANSWERS
		float[] answers = calculateAnswers (givens);
		prblm = new Problem (question, givens, answers);
		
		Debug.Log ("Max altitude = " + answers [1] + " Time above ground = " + answers [0]); 
	}
	public void initFieldAccess()
	{
		main_gui = GameObject.Find ("GUI"); main_gui.SetActive (false);
		height_input = GameObject.Find ("HEIGHT_INPUT/numpad_inputfield/Text");
		time_input = GameObject.Find ("TIME_INPUT/numpad_inputfield/Text");
		question_panel_text = GameObject.Find ("Question Panel/Text");
	}
	
	public float[] calculateAnswers(float[] givens)
	{
		/* Step 1: Find time required to reach altitude of 1000m */
		// d = v*t + 0.5*a*t^2 (Vertically)
		// 0.5*a*t^2 + v*t - 1000 = 0
		float t_part1 = (-1f*givens[0] + Mathf.Sqrt ((float) (givens[0]*givens[0] - 4f * (0.5f*givens[1]) * -1000f) )) / (2 * (0.5f*givens[1]));

		/* Step 2: Find initial velocity at 1000m */
		// vf = vi + a*t
		float vf = givens[0] + givens[1] * t_part1;

		
		/* Step 3: Find time required to reach altitude of 0m */
		// 0 = 1000 + vf*t -0.5*9.8*t^2
		float t_part2 = (-1f*vf - Mathf.Sqrt ((float) (vf*vf - 4f * -(0.5f*9.8f) * 1000f) )) / (2f * -(0.5f*9.8f));

		/* Step 4: Find the max altitude */
		// Reached when velocity = 0
		// h = 1000 + vf^2/2*9.8
		float max_altitude = ( (vf * vf) / (2f * 9.8f)) + 1000f;

		/* Step 5: Find total time*/
		float total_time = t_part1 + t_part2;

		return new float[] { total_time , max_altitude };
	}
	public void displayQuestion()
	{
		question_panel_text.GetComponent<Text> ().text = prblm.getQuestion ();
	}
	public void onEnter()
	{
		float time_submission = float.Parse(time_input.GetComponent<Text>().text);
		float altitude_submission = float.Parse(height_input.GetComponent<Text> ().text);
		
		bool correct = checkSubmission (time_submission, altitude_submission);
		
		if (correct) 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(-30);
			CharacterHealth.inSequence = false;

			Game_Loop.points += 500;
			Game_Loop.problem_in_sequence = false;
			Game_Loop.finished_problems++;

			StartCoroutine(endSequence(GameObject.Find("Rocket").GetComponent<Rigidbody2D>()));
		} 
		else 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(30);
			height_input.GetComponent<Text>().text = "HEIGHT";
			time_input.GetComponent<Text> ().text = "TIME";
		}
	}
	public IEnumerator endSequence() { return null; }
	public IEnumerator endSequence(Rigidbody2D body)
	{
		body.AddForce(new Vector2(0, 1000));

		yield return new WaitForSeconds(1);

		main_gui.SetActive(true);
		Destroy( GameObject.Find("LAUNCH_ENV(Clone)")); 
		Destroy (this.gameObject);
	}
	bool checkSubmission(float total_time, float max_altitude)
	{
		bool time_correct = prblm.getAnswers () [0] >= total_time - 5
			&& prblm.getAnswers () [0] <= total_time + 5;
		
		bool altitude_correct = prblm.getAnswers () [1] >= max_altitude - 5
			&& prblm.getAnswers () [1] <= max_altitude + 5; 
		
		if ( time_correct && altitude_correct)
		{
			return true;
		}
		return false;
	}
}
