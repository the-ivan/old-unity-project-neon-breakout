using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private GameManager GM;
    private LevelBuilder level_builder_script;  // Reference to level builder
	
	void Awake ()
	{
		GM = GameManager.instance;
        level_builder_script = GameObject.FindGameObjectWithTag(Tags.game_manager).GetComponent<LevelBuilder>();
        DontDestroyOnLoad(this); // to allow the scene to change
	}

	// Main menu buttons
	public void LaunchGameButton()
	{
		GM.SetToLevelPicker();
        Destroy(transform.gameObject);
	}
	public void LevelEditorButton()
	{
        StartCoroutine(LoadAndBuildEditorRoutine());
	}
    IEnumerator LoadAndBuildEditorRoutine()
    {
        Application.LoadLevel(4);           // 4 is the editor scene
        yield return null;
        level_builder_script.BuildLevel(0); // 0 is the editor level
        GM.SetToLevelEditor();

        Destroy(transform.gameObject);
    }
	public void QuitGameButton()
	{
		Application.Quit();
	}

    // General buttons
    public void BackButton()
    {
        //TODO: code back button
        Destroy(transform.gameObject);
    }
}
