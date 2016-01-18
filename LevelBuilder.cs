using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject wall;			// Wall prefabs
    [SerializeField]
    private GameObject floor;			// Invisible floor 
    [SerializeField]
    private GameObject ball;			// Ball object
    [SerializeField]
    private GameObject paddle;			// Paddle object

    private CameraSetup camera_script;	// Reference to camera script
    private Grid grid_script;			// Reference to grid script
	private GameManager GM;				// Reference to game manager

    private GameObject[] wall_array;	// Temporary storage of wall objects for scaling
	private GameObject actual_ball;		// And ball
	private GameObject actual_paddle;	// And paddle
	
	void Awake () 
	{
		GM = GameManager.instance;
		camera_script	= GameObject.FindGameObjectWithTag(Tags.main_cam).GetComponent<CameraSetup>();
		grid_script		= GameObject.FindGameObjectWithTag(Tags.game_manager).GetComponent<Grid>(); 
		
		// Instantiate variables
		wall_array = new GameObject[5];
	}
	
	void Update () 
	{
		
	}

	public void BuildLevel(int level)
	{
		// Brick Grid
        grid_script.BuildGrid(level);
		
		// Walls
		// Left Wall
		wall_array[0] = (GameObject)Instantiate(wall, new Vector3(camera_script.brick_width / 4, Screen.height / 2, 0), Quaternion.identity);
		wall_array[0].transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);
		// Top Wall
		wall_array[1] = (GameObject)Instantiate(wall, new Vector3(Screen.width / 2, Screen.height - camera_script.brick_width / 4, 0), Quaternion.Euler(0, 0, 90));
		wall_array[1].transform.localScale = new Vector3(camera_script.scale_factor, 2, 0);
		// Middle Wall
		wall_array[2] = (GameObject)Instantiate(wall, new Vector3(camera_script.brick_width * 20 + camera_script.brick_width * 3 / 4, Screen.height / 2, 0), Quaternion.identity);
		wall_array[2].transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);
		// Right Wall
		wall_array[3] = (GameObject)Instantiate(wall, new Vector3(Screen.width - camera_script.brick_width / 4, Screen.height / 2, 0), Quaternion.identity);
		wall_array[3].transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);
		// Bottom Wall
		wall_array[4] = (GameObject)Instantiate(floor, new Vector3(Screen.width / 2, camera_script.brick_height * 2, 0), Quaternion.identity);
		
        if (level != 0)
        {
            // Paddle
            actual_paddle = (GameObject)Instantiate(paddle, new Vector3(Screen.width * 3 / 8, Screen.height / 16, 0), Quaternion.identity);
            actual_paddle.transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);

            // Ball
            actual_ball = (GameObject)Instantiate(ball, new Vector3(Screen.width * 3 / 8, Screen.height / 16 + camera_script.brick_height, 0), Quaternion.identity);
            actual_ball.transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);
        }
	}
}