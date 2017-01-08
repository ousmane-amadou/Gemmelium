using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class engQ  {
	string[] KQ = {
		"According to the scientific definition of work, pushing on a rock accomplishes no work unless there is",
		"The potential energy of a box on a shelf, relative to the floor, is a measure of",
		"Which quantity has the greatest influence on the amount of kinetic energy that a car has while moving down the highway?",
		"Most energy comes to and leaves the earth in the form of",
		"Energy is",
		"If the speed of an object is doubled then its kinetic energy is",
		"The type of energy possessed by a simple pendulum, when it is at the mean position is",
		"If air resistance is negligible, the sum total of potential and kinetic energies of a freely falling body",
		"A rifle fires a bullet. Immediately after the bullet is fired, which of the following is NOT true?",
		"Two objects of equal mass hit a wall with the same velocity. They both rebound with the same velocity. Both objects experience the same momentum change but one experiences twice the force as the other.Which of the following statements is true?",
		"In any collision",
		"If the momentum of a ball is doubled, then the kinetic energy is"
	};

	string[,] KA = {
		{  "movement in the same direction as the force.", "an applied force greater than its weight.", "a net force greater than zero.", "an opposing force." },
		{ "any of the choices", "the work done putting the box on the shelf from the floor.", "the weight of the box times the distance above the floor.", 
			"the energy the box has because of its position above the floor." },
		{ "velocity", "mass" , "weight", "size"},
		{ "nuclear energy.", "chemical energy.", "radiant energy.", "kinetic energy." },
		{ "all the options", "the ability to do work.", "the work needed to create potential or kinetic energy.", "the work that can be done by an object with PE or KE."},
		{ "halved", "doubled", "quadrupled", "tripled" },
		{ "kinetic energy", "potential energy", "potential energy + kinetic energy", "sound energy" },
		{ "increases", "decreases", "becomes zero", "remains the same" },
		{ "the rifle and the bullet have the same kinetic energy", "the impulse on the rifle due to the bullet and the impulse on the bullet due to the rifle have the same magnitude",
			"the force on the rifle due to the bullet and the force on the bullet due to the rifle have the same magnitude", "the rifle and the bullet have the same magnitude of momentum" },
		{ "the contact time between the object and the wall of one is twice the contact time of the other", "the contact time between the object and the wall of one is 1/3 the contact time of the other",
		  "the contact time between the object and the wall of one is equal to the contact time of the other", "the contact time between the object and the wall of one is one-half the contact time of the other." },
		{ "total momentum is conserved.", "total kinetic energy is conserved.", ". total momentum is not conserved.", "total momentum and total kinetic energy are conserved and the masses are equal." },
		{ "3 times larger", "0.5 times larger", "4 times larger", "2 times larger"}
	};

	public string question;
	public string[] answers = { "", "", "", "" };
	public int correct_answer; 
	public int question_value;

	// Use this for initialization
	public engQ()
	{
		int n = Random.Range (0, 2);
		switch (n) {
		case 0:
			generateT1 ();
			break;
		case 1:
			generateTK ();
			break;

		}
	}
	void generateTK() {
		int rnd = Random.Range (0, 11);
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

	void generateT1()
	{
		int n = Random.Range (1, 2), rnd, rnd2;
		float mass, height, kinEnergy;
		string[] s, a;
		List<int> ids;

		switch (n) {

		//1. Generate Random choice. 
		//2. Set the question to the randomly chosen choice
		//3. (a) Randomly chose and index to place the correct answer for
		//3. (b) Randomly fill each index, with randomly chosen answers
		case 1: //The SI Unit of ____ is:
			mass = Random.Range (10f, 1000f);
			height = Random.Range (10f, 1000f);
			s = new string[] { "mm", "cm", "km", "m" };

			rnd = Random.Range (0, 3);

			question = "A " + mass.ToString() + "kg falls from a height of " + height.ToString() + s[rnd] + " The momentum of the mass just before it hits the ground is ";
			question_value = 10;
			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					if (rnd == 0) { /* momentum = mass * final velocity */
						answers [ids [rnd2]] = (Mathf.Sqrt (height / 9.8f) * 9.8f * mass * 1000).ToString () + " Ns";
					} else if (rnd == 1) {
						answers [ids [rnd2]] = (Mathf.Sqrt (height / 9.8f) * 9.8f * mass * 100).ToString () + " Ns";
					} else if (rnd == 2) {
						answers [ids [rnd2]] = (Mathf.Sqrt (height / 9.8f) * 9.8f * mass / 1000).ToString () + " Ns";
					} else {

					}


					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				} else {
					answers [ids[rnd2]] = (answers [ids[rnd2]] + Random.Range(1, 10) + Random.Range(0f,10f)*10.000).ToString() + " Ns";
					ids.RemoveAt (rnd2);
				}
			}
			break;
		case 2: 
			mass = Random.Range (10f, 1000f);
			kinEnergy = Random.Range (10f, 1000f);

			s = new string[] { "g", "kg", "mg", "tonnes" };

			rnd = Random.Range (0, 3);

			question = "A " + mass.ToString() + " " + s[rnd] + " ball is pitched with a kinetic energy of " + kinEnergy.ToString() + " joules. The momentum of the ball is ";
			question_value = 10;
			ids = new List<int> {0,1,2,3};
			for (int i = 0; i < 4; i++) 
			{
				rnd2 = Random.Range (0, 3 - i);
				if (i == 0) {
					if (rnd == 0) { /* momentum = mass * final velocity */
						answers [ids [rnd2]] = (Mathf.Sqrt (mass / kinEnergy) * mass * 1000).ToString () + " Ns";
					} else if (rnd == 1) {
						answers [ids [rnd2]] = (Mathf.Sqrt (mass / kinEnergy) * mass).ToString () + " Ns";
					} else if (rnd == 2) {
						answers [ids [rnd2]] = (Mathf.Sqrt (mass / kinEnergy) * mass * 1000000).ToString () + " Ns";
					} else {
						answers [ids [rnd2]] = (Mathf.Sqrt (mass / kinEnergy) * mass / 1000).ToString () + " Ns";
					}

					correct_answer = rnd2;
					ids.RemoveAt (rnd2);
				} else {
					answers [ids[rnd2]] = (answers [ids[rnd2]] + Random.Range(1, 10) + Random.Range(0f,10f)*10.000).ToString() + " Ns";
					ids.RemoveAt (rnd2);
				}
			}
			break;

		case 3: 
			break;

		case 4:
			break;
		}

	}
}

