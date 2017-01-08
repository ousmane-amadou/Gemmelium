using UnityEngine;
using System.Collections;


/**
 * <summary>
 * This class handles how coin's in the game behave. <br>
 * Whenever a coin is touched by the player, it is destroyed and
 * 5 points are added.
 * </summary>
 */ 
public class Coin : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy (this.gameObject);
		Game_Loop.points += 5;
	}
}
