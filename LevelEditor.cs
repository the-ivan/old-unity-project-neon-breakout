using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelEditor : MonoBehaviour {

    private LevelBuilder level_builder_script;  // Reference to level builder
    private CameraSetup camera_script;	        // Reference to camera script
    private Grid grid_script;			        // Reference to grid script
    private Levels levels_script;               // Reference to levels script
    private string level_name;                  // Custom level name
    // TODO: custom level names regex check for invalid chars

	void Awake ()
	{
		camera_script        = GameObject.FindGameObjectWithTag(Tags.main_cam).GetComponent<CameraSetup>();
        level_builder_script = GameObject.FindGameObjectWithTag(Tags.game_manager).GetComponent<LevelBuilder>();
        grid_script          = GameObject.FindGameObjectWithTag(Tags.game_manager).GetComponent<Grid>();
        levels_script        = GameObject.FindGameObjectWithTag(Tags.game_manager).GetComponent<Levels>();
	}

    void LocateGridCell (float x, float y, bool left_click)
    {
        if (x >= camera_script.brick_width / 2 && x <= (3f / 4f * Screen.width) && y >= (1f / 3f * Screen.height) && y <= (Screen.height - camera_script.brick_height))
        {
            int i = Mathf.FloorToInt((x - camera_script.brick_width / 2) / camera_script.brick_width);
            if (i == 20) i = 19;
            int j = Mathf.FloorToInt((y - ((Screen.height - camera_script.brick_height) / 3f)) / camera_script.brick_height);
            if (j == 20) j = 19;

            if (left_click)
            {
                if ((grid_script.brick_type[i, j] + 1) == grid_script.max_types) // cycles back to no brick if limit of brick types is reached
                {
                    grid_script.brick_type[i, j] = 0;
                    
                    if (grid_script.brick_array[i, j] != null)
                    {
                        DestroyObject(grid_script.brick_array[i, j].gameObject);
                    }
                }
                else // If block present, destroy it, and build new one
                {
                    if (grid_script.brick_array[i,j] != null)
                    {
                        DestroyObject(grid_script.brick_array[i, j].gameObject);
                    }
                    grid_script.brick_type[i, j] += 1;
                    grid_script.SpawnBrick(grid_script.brick_type[i, j], i, j);
                }
            }
            else if (!left_click)
            {
                if (grid_script.brick_array[i, j] != null)
                {
                    DestroyObject(grid_script.brick_array[i, j].gameObject);
                    grid_script.brick_type[i, j] = 0;
                }
            }
        }
    }

	void Update () {
	
		// Mouse click - check region of screen and find corresponding location in the 2D array
		// Cycle through different brick types with the clicks on the same area.
		// TODO: If mouse is held down, should spawn bricks as it moves.

		if (Input.GetMouseButtonDown(0))
		{
            LocateGridCell(Input.mousePosition.x, Input.mousePosition.y, true);
        }
        
        if (Input.GetMouseButtonDown(1))
		{
            LocateGridCell(Input.mousePosition.x, Input.mousePosition.y, false);
		}
	}

    // Set Name Button/Field
    // TODO: rework custom level name setting (modal-like confirmation dialog)
    public void SaveNameButton()
    {
        level_name = GetComponentInChildren<InputField>().text;
    }


    // Save Button
    public void SaveButton()
    {
        // Save level in editor
        levels_script.SaveGrid(level_name, grid_script.brick_type);
        Debug.Log("level saved as: " + level_name);
        // TODO: add save dialog, and a save confirmation
    }

    // Reset Button
    public void ResetButton()
    {
        // Reset level in editor
        // TODO: add a button to reset level editor grid
    }
}