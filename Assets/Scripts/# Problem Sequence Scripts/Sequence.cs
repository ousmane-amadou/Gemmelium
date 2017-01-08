using UnityEngine;
using System.Collections;

/**
 * @author Ousmane Amadou
 * @since 1.0
 * 
 * <summary>
 * Every sequence in the game will need to: <br>
 * 		--> Be able to display Givens, Question, and Generate theier own problems <br>
 * 		--> Have access to their own input_pad <br>
 * 		--> Setup a specialized environment for the user to expereince
 * </summary>
 * 
 * Expectation Coverage: <br>
 * 
 * A1 – Data Types and Expressions <br>
 * Demonstrate the ability to use different data types 
 * and expressions when creating computer programs
 * 
 * A4 – Code Maintenance<br>
 * create fully documented program codecreate clear and 
 * maintainable external user -documentation
 * 
 * C1 – Modular Design <br>
 * demonstrate the ability to apply modular design 
 * concepts in computer programs
 */
public interface Sequence 
{
	// Initiliazes the environment in which all the physics object will interact
	void initEnvironment();

	// Grants the sequence access to all UI element. (Assigning references)
	void initFieldAccess();

	/**
	 * @param g an array of required givens to calculate answer
	 */ 
	float[] calculateAnswers(float[] g);

	/**
	 * <summary>
	 * Generates a problem for the player to solve.
	 * Every sequence will have a unique way of generating their problem because every sequence will have their
	 * own types of problems (work, projectile, rocket....) to generate.
	 * </summary> 
	 */
	void generateProblem();

	/**
	 * Precondition: All Field's are accessible, and appropriate input is inputted (integers)
	 * Postcondtion: A lose of health, or gaining of health + completion of sequence
	 * 
	 * <summary>
	 * 1. Check's to see if submission is correct
	 * 2. If not, deplete health.. If so move on to step 3.
	 * 3. a. Gain Health
	 *    b. Stop Constant Health Depletion
	 *    c. Add 500 Points
	 * 	  d. End Sequence
	 * </summary> 
	 */
	void onEnter();

	// Displays the question the player needs to solve
	void displayQuestion();

	// The process required to end the sequence
	IEnumerator endSequence ();
}
