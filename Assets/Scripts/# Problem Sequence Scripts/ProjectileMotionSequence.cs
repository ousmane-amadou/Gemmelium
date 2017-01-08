using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectileMotionSequence : MonoBehaviour, Sequence
{
	public Problem prblm;
	public Transform sequence_env;
	public GameObject control_gui;

	private GameObject distance_input, time_input, question_panel_text;
	private GameObject main_gui;

	private GameObject question_text;

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
		GameObject.Find ("PMOTION_ENV(Clone)").transform.SetParent (Camera.main.transform);
	}

	public void generateProblem()
	{
		float[] givens = new float[2];
		givens[0] = Random.Range(5, 10) * 1f; // INITIAL VELOCITY
		givens[1] = Random.Range(5, 90) * 1f; // ANGLE

		string question = 
			"John kicks the ball and ball does projectile motion with an angle " + givens[1] + "º " +
			"to horizontal. Its initial velocity is " + givens[0] +"m/s, find the maximum height it can " +
			"reach, horizontal displacement and total time required for this motion. ";

		//CALCULATE ANSWERS
		float[] answers = calculateAnswers (givens);
		prblm = new Problem (question, givens, answers);
	}
	public void initFieldAccess()
	{
		main_gui = GameObject.Find ("GUI"); main_gui.SetActive (false);
		distance_input = GameObject.Find ("DISPLACEMENT_INPUT/numpad_inputfield_d/Text");
		time_input = GameObject.Find ("TIME_INPUT/numpad_inputfield/Text");
		question_panel_text = GameObject.Find ("Question Panel/Text");

	}

	public float[] calculateAnswers(float[] givens)
	{
		float angle_in_radians = (Mathf.PI / 180) * givens [1];
		float Vx = givens[0] * Mathf.Cos (angle_in_radians);
		float Vy = givens[0] * Mathf.Sin (angle_in_radians);

		/* Step 1: Find the time required to reach peak and multiply by 2 */
		float t = (Vy / 9.8f) * 2;

		/* Step 2: Find the horizontal displacement */
		float horizontal_displacement = Vx * t;

		Debug.Log (t + ", " + horizontal_displacement); 
		return new float[] { t , horizontal_displacement };
	}
	public void displayQuestion()
	{
		question_panel_text.GetComponent<Text> ().text = prblm.getQuestion ();
	}
	public void onEnter()
	{
		float distance_submission = float.Parse(distance_input.GetComponent<Text> ().text);
		float time_submission = float.Parse(time_input.GetComponent<Text>().text);
		
		bool correct = checkSubmission (distance_submission, time_submission);

		if (correct) 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(-30);
			CharacterHealth.inSequence = false;

			Game_Loop.points += 500;
			Game_Loop.problem_in_sequence = false;
			Game_Loop.finished_problems++;

			StartCoroutine(endSequence(GameObject.Find("projectile").GetComponent<Rigidbody2D>()));
		} 
		else 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(30);
			distance_input.GetComponent<Text>().text = "DISPLACEMENT";
			time_input.GetComponent<Text> ().text = "TIME";
		}
	}
	public IEnumerator endSequence(){return null;}
	public IEnumerator endSequence(Rigidbody2D body)
	{
		body.AddForce(new Vector2(350, 500));

		yield return new  WaitForSeconds(1);

		Destroy (GameObject.Find("PMOTION_ENV(Clone)")); 
		main_gui.SetActive(true);
		Destroy (this.gameObject);
	}
	bool checkSubmission(float displacement, float time)
	{
		bool displacement_correct = prblm.getAnswers () [1] >= displacement - 5
			&& prblm.getAnswers () [1] <= displacement + 5;

		bool time_correct = prblm.getAnswers () [0] >= time- 5
			&& prblm.getAnswers () [0] <= time + 5; 

		if (displacement_correct && time_correct)
		{
			return true;
		}
		return false;
	}
}
