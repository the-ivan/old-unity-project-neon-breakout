// TODO: 
/*
pause_screen on_pause function:
set alpha of UI pause element to 1
	
pause_screen on_live function:
set alpha of UI pause element to 0
*/
// TODO: Escape button more robust
// TODO: Wire in what happens at game end


using UnityEngine;
using System.Collections;

public enum GameState {Intro, Menu, Level_Picker, Pre_Game, Live_Game, Pause_Game, End_Game, Level_Editor}

public class GameManager : MonoBehaviour {
	// Private reference only this class can access
	private static GameManager _instance;
	
	// Public reference that other classes will use
	public static GameManager instance 
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				// Do not destroy this object when loading a new scene
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	
	public GameState game_state { get; private set; }
	public GameState old_state  { get; private set; }
	public delegate void EventHandler();
	public event EventHandler IntroState;
	public event EventHandler MenuState;
	public event EventHandler PreGameState;
	public event EventHandler LiveGameState;
	public event EventHandler PauseGameState;
	public event EventHandler EndGameState;
	public event EventHandler LevelEditorState;
	
	private bool new_game 	= true;
	private int lives 		= 0;
	private int cur_level 	= 0;
	
	void Awake ()
	{
		if (_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If the Singleton already exists, destroy this
			if (this != _instance) Destroy(this.gameObject);
		}
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Escape"))
		{
            if (game_state == GameState.Live_Game)
            {
                SetToPauseGame();
            }
            else if (game_state == GameState.Pause_Game)
            {
                SetToLiveGame();
            }
		}
	}

	void ChangeState(GameState new_state)
	{
		old_state = this.game_state;
		this.game_state = new_state;
        #if UNITY_EDITOR
        Debug.Log("Changing from " + old_state + " to " + new_state);
        #endif

	}

    public void LoseLife()
    {
        lives -= 1;
        if (lives != 0)
        {
            SetToPreGame();
        }
        else
        {
            SetToEndGame();
        }
    }

	public void SetToMenu()
	{
		Application.LoadLevel(1);
		ChangeState(GameState.Menu);
		if (MenuState != null) { MenuState(); }
	}
	
	public void SetToLevelPicker()
	{
		Application.LoadLevel(2);
		ChangeState(GameState.Level_Picker);
	}

	public void SetToPreGame()
	{
		ChangeState(GameState.Pre_Game);
		if (PreGameState != null) { PreGameState(); }
				
		// New game setup
		if (new_game)
		{
			lives = 2;
			cur_level = 0;
			new_game = false;
		}
		else if (!new_game && lives == 0)
		{
			SetToEndGame();
		}
	}
	
	public void SetToLiveGame()
	{
		ChangeState(GameState.Live_Game);
		if (LiveGameState != null) { LiveGameState(); }
	}
	
	public void SetToPauseGame()
	{
		ChangeState(GameState.Pause_Game);
		if (PauseGameState != null) { PauseGameState(); }
	}
	
	public void SetToEndGame()
	{
        ChangeState(GameState.End_Game);
		if (EndGameState != null) { EndGameState(); }
        new_game = true;
	}
	
	public void SetToLevelEditor()
	{
		ChangeState(GameState.Level_Editor);
		if (LevelEditorState != null) { LevelEditorState(); }
	}
}