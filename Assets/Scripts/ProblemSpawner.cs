using UnityEngine;
using System.Collections;

public class ProblemSpawner : MonoBehaviour 
{
	public GameObject problem_object;
	private Vector3 position;
	// Use this for initialization
	void Start () 
	{
		position = GetComponent<BoxCollider2D> ().transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.gameObject.tag == "chosen" && Game_Loop.problem_in_sequence == false)
		{
			Instantiate(problem_object, position, Quaternion.identity);
			GenerationLogic.gen_profile = this.gameObject.name;
			Game_Loop.current_spawn_point = this.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Destroy(GameObject.Find ("ProblemObject(Clone)"));
	}
}
