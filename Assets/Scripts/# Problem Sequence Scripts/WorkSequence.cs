using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorkSequence : MonoBehaviour, Sequence
{
	public Problem prblm;
	public Transform sequence_env;
	
	private GameObject work_input, question_panel_text;
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
		GameObject.Find ("WORK_ENV(Clone)").transform.SetParent (Camera.main.transform);
	}
	
	public void generateProblem()
	{
		float[] givens = new float[4];

		givens[0] = Random.Range(1, 10) * 1f; 	// MASS OF BOX (in KG)
		givens[1] = Random.Range(1, 10) * 0.1f; // COOEFFICEINT OF FRICTON
		givens[2] = Random.Range(20, 50) * 1f;  // APPLIED FORCE
		givens[3] = Random.Range(5, 40) * 1f;  	// DISPLACEMENT

		//Ensures that there is enough netforce to move crate forward
		while ( (givens[0]*givens[1]*-9.8f + givens[2]) < 0) 
		{
			givens[0] = Random.Range(1, 10) * 1f; 	// MASS OF BOX (in KG)
			givens[1] = Random.Range(1, 10) * 0.1f; // COOEFFICEINT OF FRICTON
			givens[2] = Random.Range(20, 50) * 1f;  // APPLIED FORCE
		}
	
		string question = 
			"Find the amount of work required to move an object given that: " +  
			"\nMass of Box = " + givens [0] + " kg " +
			"\nCoefficient of Friction = " + givens [1] + 
			"\nApplied Force = " + givens [2] + " N " +
			"\nDisplacement = " + givens [3] + " m "; 
		
		//CALCULATE ANSWERS
		float[] answers = calculateAnswers (givens);
		prblm = new Problem (question, givens, answers);
		
		Debug.Log ("Work = " + answers [0]);
	}
	public void initFieldAccess()
	{
		main_gui = GameObject.Find ("GUI"); main_gui.SetActive (false);
		work_input = GameObject.Find ("DISPLACEMENT_INPUT/numpad_inputfield_d/Text");
		question_panel_text = GameObject.Find ("Question Panel/Text");
	}
	
	public float[] calculateAnswers(float[] givens)
	{
		/* Step 1: Calculate Frictional Force */
		// Ff = uFn = coefficient of friction * mass * gravitational constant
		float frictional_force = givens[1] * (givens[0] * -9.8f);
		
		/* Step 2: Calculate net force */
		// Fn = Fa + Ff
		float net_force = frictional_force + givens [2];

		/* Step 3: Calculate Work */
		// W = Fn*d
		float work = net_force * givens [3];
		
		return new float[] { work };
	}
	public void displayQuestion()
	{
		question_panel_text.GetComponent<Text> ().text = prblm.getQuestion ();
	}
	public void onEnter()
	{
		float work_submission = float.Parse(work_input.GetComponent<Text>().text);
		
		bool correct = checkSubmission (work_submission);
		
		if (correct) 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(-30);
			CharacterHealth.inSequence = false;

			Game_Loop.points += 500;
			Game_Loop.problem_in_sequence = false;
			Game_Loop.finished_problems++;

			StartCoroutine(endSequence(GameObject.Find("CRATE").GetComponent<Rigidbody2D>()));
		} 
		else 
		{
			GameObject.Find("health").GetComponent<CharacterHealth>().takeDamage(30);
			work_input.GetComponent<Text>().text = "WORK";
		}
	}
	public IEnumerator endSequence() { return null; }
	public IEnumerator endSequence(Rigidbody2D body)
	{
		body.AddForce(new Vector2(1000, 0));

		yield return new WaitForSeconds(1);

		main_gui.SetActive(true);
		Destroy( GameObject.Find("WORK_ENV(Clone)")); 
		Destroy (this.gameObject);
	}

	bool checkSubmission(float work)
	{
		bool work_correct = prblm.getAnswers () [0] >= work - 5
			&& prblm.getAnswers () [0] <= work + 5;

		if (work_correct)
		{
			return true;
		}
		return false;
	}
}
