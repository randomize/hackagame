﻿using UnityEngine;  

public class GameController : MonoBehaviour
{

    public enum GameState 
    {
        InGame,
        GameOver
    }

    public GameState state;
    public int gameScore;
    public GameObject hen;

    public TextMesh gameOverLabel;
    public TextMesh gameOverScoreLabel;
    public TextMesh scoreLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 henPosition = hen.transform.position;
	    Vector3 toScreen = Camera.main.WorldToScreenPoint(henPosition);

	    if (toScreen.x > Screen.width)
	    {
	        toScreen.x = 0;
            hen.transform.position = toScreen;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }
        else if (toScreen.x < 0)
        {
            toScreen.x = Screen.width;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }

	    if (toScreen.y < 0 )
	    {
	        state = GameState.GameOver;

	    }

	}
}
