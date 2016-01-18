using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	// use this script to detect collisions with the ball and delete the object and add to score
	
	//private GameManager GM;
	private int brick_type;
	
	void Awake () 
	{
		//GM = GameManager.instance;
	}
	
	public void SetType(int type)
	{
		brick_type = type;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == Tags.ball)
		{
			Destroy(transform.gameObject);
			ScoreManager.score += 10;
		}
	}
	
	void OnDisable()
	{
		//Debug.Log ("disabled game state: " + GM.game_state);
	}
}
