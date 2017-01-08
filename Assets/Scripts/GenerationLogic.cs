using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Ousmane Amadou
 * @since 1.0
 * 
 * This class works with the problem object (a green mist) to initiliaze
 * a problem sequence once the player confirms that he/she is ready to proceed. 
 */ 

public class GenerationLogic : MonoBehaviour 
{
	public GameObject ProblemButtonPrefab;
	public GameObject p_motion, rocket_launch, work, falling;
	private GameObject p_button, movement_gui;

	public static string gen_profile;

	// Use this for initialization
	void Start () 
	{
		movement_gui = GameObject.FindWithTag ("main_gui");
		p_button = GameObject.Find ("GameManager").GetComponent<Game_Loop> ().problem_button_prefab;
	}

	//Upon collision with a problem mist a lighting indicator will pop up
	void OnTriggerEnter2D(Collider2D other)
	{
		p_button.SetActive (true);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		p_button.SetActive (false);
	}

	// Clears game screen so the problem can begin 
	void clearForSequence()
	{
		Game_Loop.problem_in_sequence = true;
		Destroy (GameObject.Find ("ProblemObject(Clone)").gameObject);
	}

	/**
	 * 	Precondition: Player has entered problem mists <br>
	 *  Postcondition: A random problem sequence has been started & all standard moving guis 
	 * 				   have been removed. 
	 */
	public void OnInitiatorClick()
	{
		p_button = GameObject.Find ("GameManager").GetComponent<Game_Loop> ().problem_button_prefab;
		p_button.SetActive (false);

		initiateRandomSequence ();
		clearForSequence ();
		CharacterHealth.inSequence = true;
	}

	/**
	 * Chooses 1 out of 6 of the following problem types randomly:
	  		Projectile Motion <br>
	  		Lanuching of a rocket <br>
	  		Work <br>
	  		Falling <br>
	  		Grappling Hook 
	 */
	public void initiateRandomSequence()
	{
		List<string> options = new List<string> ();

		if(gen_profile.Contains("wrk")) { options.Add ("wrk"); }
		if(gen_profile.Contains("rck")) { options.Add ("rck"); }
		if(gen_profile.Contains("prj")) { options.Add ("prj"); }
		if(gen_profile.Contains("fall")) { options.Add ("fall"); }

		string rand_seq = options[Random.Range(0, options.Count)];
		Game_Loop.current_spawn_point.SetActive (false);

		switch (rand_seq)
		{
			case "prj":
				Instantiate(p_motion, p_motion.transform.position, Quaternion.identity);
				break;
				
			case "rck":
				Instantiate(rocket_launch,rocket_launch.transform.position, Quaternion.identity);
				break;
				
			case "wrk":
				Instantiate(work, work.transform.position, Quaternion.identity);
				break;
				
			case "fall":
				Instantiate(falling, falling.transform.position, Quaternion.identity);
				break;
		}
	}
	
}
