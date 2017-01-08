using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour 
{

	public void start_game()
	{
		SceneManager.LoadScene ("Level");
	}
	public void instructions()
	{
		SceneManager.LoadScene ("Instructions");
	}
	public void leaderboard()
	{
		SceneManager.LoadScene ("Leaderboard");
	}
}
