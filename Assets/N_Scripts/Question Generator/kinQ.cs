using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class kinQ 
{
	string[] KQ = { 
		"If car A passes car B, then car A must be ",
		"When a falling object reaches terminal velocity",
		"Is it possible for an object's velocity to increase while its acceleration decreases",
		"When an object has an acceleration of 0 m/s then the object is not",
		"If an object is travelling north than the velocity vector is ",
		"Consider drops of water that leak from a dripping faucet at a constant rate. As the drops fall they ",
		"A car and a delivery truck both start from rest and accelerate at the same rate. However, the car accelerates for twice the amount of time as the truck. What is the traveled distance of the car compared to the truck?",
		"An object is released from rest and falls in the absence of air resistance. Which of the following is true about its motion?"
	};
	string[,] KA = { 
		{ "accelerating.", "accelerating at a greater rate than car B.", "moving faster than car B and accelerating more than car B.",
			"moving faster than car B, but not necessarily accelerating." },
		{ "moves downward with constant velocity.", "is no longer subject to the friction of air.", "has no downward velocity.", 
			"has an acceleration of approximately 10 m/s^2" },
		{ "Yes. an example would be a falling object in the presence of air resistance.",
          "No. this is impossible because of the way in which acceleration is defined.",
		  "Yes. an example would be an example would be a falling object near the surface of the moon.",
			"No. because velocity would and acceleration must always be in the same direction." },
		{ "changing velocity", "changing position", "moving", "exist" },
		{ "northward", "southward", "neither", "not enough info to tell" },
		{ "get farther apart." , "get closer together.", "combust into pounds of carbohydrates.", " remain at a relatively fixed distance from one another." },
		{ "Four times as much", "Twice as much", "The same", "Half as much" },
		{ "Its acceleration is constant", " Its velocity is constant", "Its acceleration is increasing", " Its velocity is decreasing" }
	};
	public string question;
	public string[] answers = { "", "", "", "" };
	public int correct_answer; 
	public int question_value;

	// Use this for initialization
	public kinQ()
	{
		int rnd = Random.Range (1, 4);
		switch (rnd) {
		case 1:
			generateT1 ();
			break;
		case 2:
			generateT2 ();
			break;
		case 3:
			generateTK ();
			break;
		}


	}
	void generateTK() {
		int rnd = Random.Range (0, 7);
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

	void generateT2()
	{
		int num_forces = Random.Range (1, 6);
		string[] directions = { "north", "south", "east", "west" };
		string[] actors = { "bearded woman", "homosexual dinosaur", "physics student", "retarded dolphin" };

		question = "A "  + actors [Random.Range (0, 4)] + " walks ";

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

			if (i == num_forces - 1 && num_forces > 1) {
				question += "and finally " + value + " m " + directions [direction] + " ";
			} else if (num_forces > 1) {
				question += value.ToString () + " m " + directions [direction] + " then ";
			} else {
				question += value.ToString () + " m " + directions [direction] + ".";
			}
		}

		// Switch between magnitude of displacement and direction
		question += "." + "What is the magnitude of the displacement?";
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
		int n = Random.Range (1, 4), rnd, rnd2;
		string[] s, a;
		List<int> ids;

		switch (n) {
		case 1:
			s = new string[] { "velocity vs. time", "acceleration vs. time", "force vs. distance", "distance vs. time", "momentum vs. time", "xy vs. x" };
			a = new string[] { "distance travelled", "change in velocity", "work done", "absment", "impulse", "x^2*y" };

			rnd = Random.Range (0, 5);

			question = "The area under a " + s [rnd] + " is:";
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
		case 2:
			a = new string[] { "distance, time, velocity, acceleration", 
							   "distance, force, velocity, time", 
							   "distance, gravity, time, force",
							   "distance, power, time, velocity" };

			rnd = Random.Range (0, 3);

			question = "The variables used in the kinematics equations are :";
			question_value = 5;
			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids[rnd2]] = a[0];
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				}
				else {
					answers [ids[rnd2]] = a[(rnd+i)%3];
					ids.RemoveAt (rnd2);
				}
			}
			break;
		case 3:
			s = new string[] { "feathers", "iron", "gentle women", "mermaids", "goat meat", "blondes", "grapes", "rasberry coco latte" };

			rnd = Random.Range (0, 7);
			int rnd3 = Random.Range (0, 9);

			question = "What is heavier? A pound of " + s [rnd] + " or a pound of " + s[rnd3] + ".";
			question_value = 5;
			ids = new List<int> {0,1,2,3};

			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					answers [ids [rnd2]] = "Neither..";
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				} else if (i == 1) {
					answers [ids [rnd2]] = s [rnd];
					ids.RemoveAt (rnd2);
				} else if (i == 2) {
					answers [ids [rnd2]] = s [rnd3];
					ids.RemoveAt (rnd2);
				} else if (i == 3) {
					answers [ids [rnd2]] = "Insufficient Info.";
				}

			}
			break;
		case 4:
			float velocity = Random.Range (0f, 100f);
			float seconds = Random.Range (0f, 500f);
			s = new string[] { "displacement", "acceleration" };
			rnd = Random.Range (0, 1);

			question = "From rest, a car reaches a velocity of " + velocity.ToString () + " m/s in " + seconds.ToString () + "seconds."
			+ "What is the " + s [rnd] + "?";

			question_value = 5;
			ids = new List<int> {0,1,2,3};

			for (int i = 0; i < 4; i++) {
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					if (rnd == 0) {
						answers [ids [rnd2]] = (velocity * seconds).ToString ();
					}
					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				} else {
					answers [ids [rnd2]] = (velocity*seconds* Random.Range(0f,1f)+ Random.Range(-20f, 20f)).ToString();
					ids.RemoveAt (rnd2);
				}
			}
				
			break;
		




		}



	}

}
