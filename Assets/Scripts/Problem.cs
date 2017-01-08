using UnityEngine;
using System.Collections;

/*
 * @author Ousmane Amadou
 * @since 1.0
 */ 
public class Problem
{
	private float[] answers;
	private string question;
	private float[] givens;
	private GameObject sequence; 
	
	public Problem()
	{

	}
	public Problem (string q, float[] g, float[] a)
	{
		question = q;
		givens = g;
		answers = a;
	}

	
	public void setQuestion(string q) { question = q; }
	public string getQuestion() { return question; } 

	public void setGivens(float[] v) { givens = v; }
	public float[] getGivens(){ return givens; }

	public void setAnswers(float[] ans) { answers = ans; }
	public float[] getAnswers() {	return answers;		}
}

