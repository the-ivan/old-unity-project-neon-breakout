﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;
	
	private Text text;
	// add event handlers for pregame; fucking rewrite the whole thing
	
	void Awake ()
	{
		text = GetComponent<Text>();
		score = 0;
	}
	
	void Update()
	{
		//text.text = "Score\n" + score;
	}
}
