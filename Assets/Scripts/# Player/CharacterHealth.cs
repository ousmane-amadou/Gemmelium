using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour 
{
	public static int HEALTH;
	public static bool inSequence;
	public RectTransform health_slider;
	private float seconds_passed;

	// Use this for initialization
	void Start () 
	{
		HEALTH = 100;
	}


	void Update () 
	{
		seconds_passed += Time.deltaTime*0.5f;
		/* If there is a health change then this is true.
         * the health_object changes according to the direction and amount the health is meant to change
         * via this script */
		if (inSequence && seconds_passed >= 2.000f && HEALTH > 0)
		{
			takeDamage(1);
			seconds_passed = 0;
		}
	}

	/**
	 * Every time player takes a hit he loses 2 health.
	 * @param hit the amount of damage taken (1 hit = 2 health)
	 */
	public void takeDamage(int hit)
	{
		if (HEALTH - hit * 2 > 100)
		{
			hit = (HEALTH - 100)/2;
		} 
		else if (HEALTH - hit * 2 < 0)
		{
			hit = HEALTH/2;
		}

		HEALTH -= hit*2;
		health_slider.sizeDelta = new Vector3((175f - HEALTH*1.75f), 30f, health_slider.localScale.z);

	}
}
