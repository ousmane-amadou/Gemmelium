using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dynQ 
{
	string[] KQ = {
		"For an object travelling with uniform circular motion, its acceleration is",
		"Astronauts on board an orbiting space station appear to be floating because",
		"An object sits at rest on a ramp. As the angle of inclination of the ramp increases, the object suddenly begins to slide. Which of the following explanations best accounts for the object’s movement?"
	};
	string[,] KA = {
		{ "zero because the speed is constant", "directed tangent to the circle", 
			"directed toward the centre of the circle", "changing in magnitude depending on its position in the circle" },
		{ "they are in the vacuum of space", "they are outside Earth’s gravitational influence", "the force of gravity acting on them has been reduced to an insignificant level",
		  "they are in free fall along with the space station itself" },
		{ "The coefficient of static friction has decreased sufficiently.",
		  "The force of gravity acting on the object has increased sufficiently.",
		  "The component of gravity along the ramp has increased sufficiently.",
		  "The friction has decreased sufficiently while the normal force has remained unchanged." }	
	};
		
	public string question;
	public string[] answers = { "", "", "", "" };
	public int correct_answer; 
	public int question_value;

	public dynQ()
	{
		int n = Random.Range (0, 3);
		switch (n) {
		case 0:
			generateT1 ();
			break;
		case 1:
			generateT2 ();
			break;
		case 2:
			generateTK ();
			break;
			
		}
	}

	void generateTK() {
		int rnd = Random.Range (0, 2);
		List<int> ids = new List<int> {0,1,2,3};
		List<int> ids2 = new List<int> {1, 2, 3};
		question = KQ [rnd]; question_value = 5;

		for (int i = 0; i < 4; i++) 
		{
			int rnd2 = Random.Range (0, 3 - i);
			int rnd3 = Random.Range (0, 3 - i);
			if (i == 0) {
				answers [ids[rnd2]] = KA[rnd, 0];
				correct_answer = rnd2;
				ids.RemoveAt (rnd2);
			}
			else {
				answers [ids[rnd2]] = KA[rnd, ids2[rnd3]];
				ids.RemoveAt (rnd2);
				ids2.RemoveAt (rnd3);
			}
		}
	}

	// An object has x (1-5) forces acting on it: 8.4 N [S] and 7.5 N [E]. The magnitude of the net force 
	// 1. Generate number of forces acting
	// 2. For each force, generate a value, and choose a direction randomly
	// 3. Calculate net force
	void generateT2()
	{
		int num_forces = Random.Range (1, 6);
		string[] directions = { "N", "S", "E", "W" };
		question = "An object has " + num_forces + " forces acting on it: ";

		float y_component = 0f; float x_component = 0f;
		for (int i = 0; i < num_forces; i++) 
		{
			int direction = Random.Range (0, 3);
			float value = Random.Range (0f, 20f);

			if (direction == 0) {
				x_component += value;
			} else if (direction == 1) {
				x_component -= value;
			} else if (direction == 2) {
				y_component += value;
			} else if (direction == 3) {
				y_component -= value;
			}

			if (i == num_forces - 1) {
				question += value + " N [" + directions [direction] + "]." + "What is the magnitude of the net force?";
			} else {
				question += value + " N [" + directions [direction] + "], ";
			}
		}
		float magnitude = Mathf.Sqrt (x_component * x_component + y_component * y_component);

		List<int> ids = new List<int> {0,1,2,3};
		for (int i = 0; i < 4; i++) 
		{
			int rnd2 = Random.Range (0, 3 - i);
			if (i == 0) {
				answers [ids[rnd2]] = magnitude.ToString();
				correct_answer = rnd2;
				ids.RemoveAt (rnd2);
			}
			else {
				float rnd = Random.Range (-20f, 20f);
				answers [ids [rnd2]] = (magnitude + rnd).ToString();
				ids.RemoveAt (rnd2);
			}
		}
		question_value = 25;
	}
		

	void generateT1()
	{
		int n = Random.Range (1, 5), rnd, rnd2;
		string[] s, a;
		List<int> ids;

		switch (n) {

		//1. Generate Random choice. 
		//2. Set the question to the randomly chosen choice
		//3. (a) Randomly chose and index to place the correct answer for
		//3. (b) Randomly fill each index, with randomly chosen answers
		case 1: //The SI Unit of ____ is:
			s = new string[] { "force", "mass", "frequency", "energy", "power", "electric charge" };
			a = new string[] { "N", "kg", "Hz", "J", "W", "C" };

			rnd = Random.Range (0, 5);

			question = "The SI Unit of " + s [rnd] + " is:";
			question_value = 5;
			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids[rnd2]] = a[rnd];
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				}
				else {
					answers [ids[rnd2]] = a[(rnd+i)%5];
					ids.RemoveAt (rnd2);
				}
			}
			break;
		case 2: //What is the ____:
			s = new string[] {
				"force of gravity",
				"normal force",
				"frictional force",
				"tension force",
				"applied force",
				"spring force"
			};
			a = new string[] {
				"a force that attracts any object with mass", "a force perpendicular to the surfaces of objects in contact",
				"force that resists motion or attempted motion between objects in contact", "force exterted by materials, such as ropes, fibres, springs and cables",
				"a force that is applied to an object by a person or another object"
			};
			rnd = Random.Range (0, 5);

			question = "What is " + s [rnd] + "?";
			question_value = 5;
			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids[rnd2]] = a[rnd];
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				}
				else {
					answers [ids[rnd2]] = a[(rnd+i)%5];
					ids.RemoveAt (rnd2);
				}
			}
			break;
		// Generate letters to represent mass and angle
		case 3: //A child with mass __ is sliding down a slide that is inclined at an angle of __ above the horizontal. The magnitude of the normal force on the child is 
			string x = (char)(65 + Random.Range (0, 26)) + "";
			string y = (char)(65 + Random.Range (0, 26)) + "";
			a = new string[] {
				x + "gtan" + y,
				x + "" + y,
				x + "gcos" + y,
				x + "gsin" + y
			};
			rnd = Random.Range (0, 3);

			question = "A child with mass " + x + " is sliding down a slide that is inclined at an angle of " + y +
			" above the horizontal. What is the magnitude of the normal force?";
			question_value = 10;

			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids[rnd2]] = a[rnd];
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				}
				else {
					answers [ids[rnd2]] = a[(rnd+i)%4];
					ids.RemoveAt (rnd2);
				}
			}

			break;
		
		// Two ____ set out from the same spot and arrive at the same destination but they take
		// different routes. Which of the following quantities must be the same for both _____?
		case 4:
			string[] z = new string[] { "dank memes", "elaphants", "antisocial computer science students", "bearded men" };
			a = new string[] { "distance", "displacement", "accelerarion", "average velocity" };

			rnd = Random.Range (0, 3);

			question = "Two " + z [rnd] + " set out from the same spot and arrive at the same destination but they take" +
			"different routes. Which of the following quantities must be the same for both " + z [rnd] + " ?";
			question_value = 10;
			ids = new List<int> {0,1,2,3};

			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids[rnd2]] = a[1];
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				}
				else {
					answers [ids[rnd2]] = a[(1+i)%4];
					ids.RemoveAt (rnd2);
				}
			}
			break;
		}

	}
}
