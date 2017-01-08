using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Ousmane Amadou
 * @since 1.0
 * 
 * NOTE: THIS CLASS HAS BEEN TEMPORARILY DEPRECATED
 * 
 * HOW IT WORKS!
 * 1. Determine a position for all the spawned objects
 * 		a. Choose random positions from the spawn_points
 * 		b. Assign an object to that position (based on tagged landscape)
 * 2. Sort the list of objects into a queue from most left to most right objects.
 * 3a. If player moves forward, determine if the next item in the queue should be displayed
 * 	   Also note if first item in the displayed stack should be deleted.
 * 3b. If the player moves backward, determine if the previous item in the queue should be displayed
 * 	   Also note if last item in the displayed stack should be deleted.
 */

public class ObjectSpawner : MonoBehaviour 
{
	public int spawn_count;
	public float generation_radius;

	public Vector3[] object_positions;
	public BoxCollider2D[] spawn_points;
	public Landscape player_landscape;

	public List<GameObject> spawned_objects;
	Dictionary<string, GameObject> asset_library;

	// Use this for initialization
	void Start () 
	{
		//gather_resources (); 
	}

	public struct Landscape
	{
		public string scape;
		public string[] spawnable_elements;
		
		public Landscape(string scp, string[] spawnables)
		{
			scape = scp;
			spawnable_elements = spawnables;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// IF PLAYER MOVES FORWARD
		/* Iterate through the queue of spawned objects, until one within 50 pixels of the camera view if found*/




		//2. Spawn object in front of Camera View.
		//   if the spawn count hasn't been exceeded. 
		//   Make sure each object is at least x pixels away from each other.


		/*int num_choices = player_landscape.spawnable_elements.Length;
		GameObject spawned = spawnObject (player_landscape.spawnable_elements [Random.Range (0, num_choices)]);
		spawned_objects.Add (spawned);*/

		// IF PLAYER MOVES BACKWARD
	}

	void removeObject()
	{

	}
	GameObject spawnObject(string key)
	{
		return null;
	}

	/*public void change_landscape(Landscape l)
	{
		player_landscape = l;
	}*/

	/*void gather_resources()
	{
		//GameObject[] assets = GameObject.FindGameObjectsWithTag ("env");
		foreach(GameObject asset in assets)
		{
			asset_library.Add(asset.name, asset);
		}
	}*/
	/**
	 * @param float max_x the maximum possible x coordinate for a problem to be placed
	 * @param float max_x the maximum possible y coordinate for a problem to be placed
	 * @return A random location based on the given maximum x and y coordinates
	 * 
	 * Value: +1 Call for Random.Range() , +1 Call for Random.Range() 
	 * 		  +1 Creation of new object, +1 returning new object
	 */ 

	public Vector3 generatePosition(float max_x, float max_y, float min_x, float min_y)
	{
		return new Vector3 (Random.Range (min_x, max_x), Random.Range (min_y, max_y));
	}
	
	/**
	 * ALGORITHM
	 * 1. Choose Spawn Point  <br>
	 * 2. Generate Position within that spawn point <br>
	 * 2. Check If there is a Problem within an x pixel radius of that position <br>
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
	public Vector3[] generatePositions(Vector3[] pos, BoxCollider2D[] spawn_points, int radius, int num_positions)
	{
		//pos[0] = generatePosition (100, 5); //4
		int num_positions_per_landscape = 30;
		
		for (int i = 1; i < num_positions; i++) 
		{
			BoxCollider2D chosen_spawn_point = spawn_points[Random.Range(0, spawn_points.Length)];
			Vector3 position = generatePosition (chosen_spawn_point.bounds.max.x, chosen_spawn_point.bounds.max.y, 
			                                     chosen_spawn_point.bounds.min.x, chosen_spawn_point.bounds.min.y); 
			bool position_available = true;
			
			// Check if all previous positions aren't in within radius of new position
			for(int j = 0; j < i; j++)
			{
				if(position.x >= object_positions[j].x - radius 
				   && position.x <= object_positions[j].x + radius)
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