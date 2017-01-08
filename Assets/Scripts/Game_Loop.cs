using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/**
 * @author Ousmane Amadou
 * @since 1.0
 * 
 * <summary> 
 * This class manages the general elements of the game including: <br>
 * 		--> Sound
 * 		--> Problem Spawning
 * 		--> Problem Inititaion
 * 		--> Player Location
 * 		--> Point System
 * </summary>
 * 
 */
public class Game_Loop : MonoBehaviour 
{
	public Rigidbody2D player;
	public Text point_text;
	public Transform problem_object;
	public AudioClip theme;
	public AudioSource src;

	public static bool problem_in_sequence;
	public static int finished_problems = 0;
	public static int points;
	public GameObject problem_button_prefab;

	public List<Collider2D> problem_spawn_points; 
	public static GameObject current_spawn_point;

	/* Every location a problem sequence is to be generated 
	 * on the map. */ 
	private Vector3[] problem_positions = new Vector3[8];
	
	private float problem_radius = 15;
	
	// Use this for initialization
	void Start () 
	{
		src.clip = theme;
		src.Play ();

		for (int i = 0; i < 8; i++) 
		{
			int index = Random.Range (0, 12 - i);
			Collider2D chosen_spawn_point = problem_spawn_points[index];
			chosen_spawn_point.gameObject.tag = "chosen";

			Vector3 position = chosen_spawn_point.GetComponent<Transform>().position;
			problem_spawn_points.Remove (chosen_spawn_point);
			problem_positions [i] = position;
		}

		problem_in_sequence = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (src.volume < 1) 
		{
			src.volume += 1 * Time.deltaTime;
		}
		updatePoints ();

		if (finished_problems == 8) 
		{
			AddScore (points);
			SceneManager.LoadScene ("Leaderboard");
		}
	}

	/**
	 *  ALGORITHM <br>
	 * 	1. Check is the player is within a radius of a certain problem <br>
	 * 	2. Check whether a problem has already been instantiated <br>
	 *  3. a) If yes: <br>
	 * 			--> Generate a new problem in the appropriate position if there
	 * 				isn't one in existence  <br>
	 * 			--> Go back to 1 <br>
	 * 	4. b) If no: <br>
	 * 			--> Destroy any generated instance of the problem, if it is in existence <br>
	 * 			--> Go back to 1
	 * 
	 * @return Boolean representing whether player is within radius of a position
	 */
	public bool isWithinRadius(Vector3 pos, Rigidbody2D p)
	{
		if (p.position.x >= pos.x - problem_radius 
			&& p.position.x <= pos.x + problem_radius) {
			return true;
		} else {
			return false;
		}
	}
	
	/**
	 * @param float max_x the maximum possible x coordinate for a problem to be placed
	 * @param float max_x the maximum possible y coordinate for a problem to be placed
	 * @return A random location based on the given maximum x and y coordinates
	 * 
	 * Value: +1 Call for Random.Range() , +1 Call for Random.Range() 
	 * 		  +1 Creation of new object, +1 returning new object
	 * 		   
	 */ 
	public Vector3 generatePosition(float max_x, float max_y)
	{
		return new Vector3 (Random.Range (0, max_x), Random.Range (0, max_y));
	}
	/**
	 * Preconditions: 
	 * 		The Game Has Been Finished
	 * 		A List of high_scores sorted by highest number of points to lowest 
	 * 
	 * ALGORITHM
	 * 1. Retrieve the stored high_score data
	 * 	  NOTE EVERY ENTRY IS STORED AS:
	 * 		--> SCORE , 1995/07/15
	 * 2. Iterate through every score until the current games score is greater than the stored score
	 * 3. Add the score to the high_score entry
	 * 4. If no score greater is found, add the current score to the end of the high_score data
	 */ 
	public void AddScore(int points)
	{
		string score = (points - (int)(Timer.seconds * 0.1)) + "," + System.DateTime.Now.ToString ("yyyy/MM/dd");
		List<string> high_score_data;

		if (PlayerPrefs.HasKey ("high_scores")) 
		{
			high_score_data = new List<string>(PlayerPrefs.GetString ("high_scores").Split ('\n'));

			for (int i = 0; i < high_score_data.Count; i++)
			{
				int points_x = int.Parse(high_score_data[i].Split (',')[0]);

				if (points > points_x) 
				{
					high_score_data.Insert (i, score);
					break;
				}
			}
			high_score_data.Add (score);
		} 
		else 
		{
			high_score_data = new List<string> ();
			high_score_data.Add (score);
		}
			
		PlayerPrefs.SetString ("high_scores", getStrings(high_score_data) );
	}

	string getStrings(List<string> p)
	{
		string q = "";
		int i = 0;

		foreach(string r in p)
		{
			if (i != 0) {
				q += "\n";
				q += r;
				i++;
			}
			else
			{
				q += r;
				i++;
			}
		}
		return q;

	}

	public void updatePoints()
	{
		string new_points = points + "";
		new_points = ("00000").Substring(0, 5 - new_points.Length) + new_points;

		point_text.text = new_points;
	}

	/**
	 * ALGORITHM
	 * 1. Generate Position  <br>
	 * 2. Check If there is a Problem within a x pixel radius of that position <br>
	 * 		A. Check Position x with positions x-1, x-2, 0 <br>
	 * 3. if yes: go back to 1 <br>
	 * 4. If no: add position to possible problem position <br>
	 * 5. Repeat until all positions are filled
	 * 
	 * COMPLEXITY ANALYSIS <br>
	 * Line 1: +4 <br>
	 * Line 3 - 10: n <br>
	 * 		+4 <br>
	 * 		+1 <br>
	 * 		i + 1 <br>
	 * 			(+2 comparisons, +2 accessing value from array) <br>
	 * 		+1
	 * Last Line : +1 <br>
	 * O(n) = 4 + n(4+1+ (i + 1)(4) + 1 <br>
	 * O(n) = 4 + n(5 +4i+ 4) +1 <br>
	 * O(n) = 5 + 9n + 4ni +1 <br>
	 * O(n) = 6 + 9n + 4ni <br>
	 * O(n) = 4ni + 9n + 6 <br>
	 * O(n) = 4ni 
	 */
	public Vector3[] generatePositions(Vector3[] pos, float radius, List<BoxCollider2D> spawn_ps)
	{
		pos[0] = generatePosition (100, 5); //4
		int num_positions = 6;

		for (int i = 1; i < num_positions; i++) 
		{
			Vector3 position = generatePosition (100, 5); 
			bool position_available = true;

			// Check if all previous positions aren't in within radius of new position
			for(int j = 0; j < i; j++)
			{
				if(position.x >= problem_positions[j].x - radius 
				   && position.x <= problem_positions[j].x + radius)
				{
					position_available = false;
					break;
				}
			}

			// If the position is available add it to the list of generated positions
			// If no go back and try again.
			if(position_available)
			{
				pos[i] = position;
			}
			else
			{
				i--;
			}
		}
		return pos;
	}

}