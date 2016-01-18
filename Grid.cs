using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public int[,] brick_type            { get; private set; }   // Brick type
    public GameObject[,] brick_array    { get; private set; }	// 2D array of brick game objects
    public int max_types                { get; private set; }   // Brick types possible (-1), will increase with development

    [SerializeField]
    private GameObject brick_1hp;		// Brick - 1HP

    private CameraSetup camera_script;	// Script attached to MC
    private Levels levels;              // Reference to levels script

	void Awake() 
    {
        camera_script = GameObject.FindGameObjectWithTag(Tags.main_cam).GetComponent<CameraSetup>();
        levels = GetComponent<Levels>();
        brick_array = new GameObject[20, 20];
        max_types = 2;
	}
	
	void Update()
    {
	    
	}

    public void BuildGrid(int level)
    {
        brick_type = levels.LoadGrid(level);

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                SpawnBrick(brick_type[i, j], i, j);
            }
        }
    }

    public void SpawnBrick(int i, int x, int y)
    // 0 - "empty" brick - implemented
    // 1 - 1hp brick - implemented
    // 2 - 2hp brick
    // 3 - 3hp brick
    // 4 - invincible brick
    // 5 - brick hittable from top only?
    // 6 - teleporter A
    // 7 - teleporter B
    {
        // The following sets the x and y locations for the brick to be spawned
        float x_location = camera_script.brick_width + camera_script.brick_width * x;
        float y_location = Screen.height / 3 + camera_script.brick_height * y;

        switch (i)
        {
            case 0:
                // Empty cell
                break;

            case 1:
                // 1HP Brick
                brick_array[x, y] = (GameObject)Instantiate(brick_1hp, new Vector3(x_location, y_location, 0), Quaternion.identity);
                brick_array[x, y].transform.localScale = new Vector3(camera_script.scale_factor, camera_script.scale_factor, 0);
                brick_array[x, y].GetComponent<Brick>().SetType(1);
                break;

            default:
                Debug.Log("Default Case thrown in SpawnBrick");
                break;
        }
    }
}
