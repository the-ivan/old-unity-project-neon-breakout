using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    [SerializeField]
	private float speed = 400f;

    private GameManager GM;
    private CameraSetup camera_script;
    
    private Vector3 movement;
	private bool collision_r_wall;
	private bool collision_l_wall;
	
	void OnEnable()
	{
		GM = GameManager.instance;
		GM.PreGameState += PaddlePreGame;
		GM.LiveGameState += PaddleLiveGame;
		GM.PauseGameState += PaddlePauseGame;
	}
	
	void Awake()
	{
        camera_script = GameObject.FindGameObjectWithTag(Tags.main_cam).GetComponent<CameraSetup>();
	}

	void Update()
	{
        if (GM.game_state != GameState.Pause_Game)
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (collision_r_wall == true && h > 0f) h = 0f; // prevents going past right wall
            if (collision_l_wall == true && h < 0f) h = 0f; // prevents going past left wall
            Move(h);
        }
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == Tags.wall && transform.position.x > Screen.width / 2) collision_r_wall = true;
		if (coll.gameObject.tag == Tags.wall && transform.position.x < Screen.width / 2) collision_l_wall = true;
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == Tags.wall)
		{
			collision_r_wall = false;
			collision_l_wall = false;
		}
	}
	
	void PaddlePreGame()
	{
		// ionno yet
	}
	void PaddleLiveGame()
	{
		// ionno yet
	}
	
	void PaddlePauseGame()
	{
		// stop movement
	}
	
	void Move(float h)
	{
		movement.Set(h, 0f, 0f);
		movement = movement.normalized * speed * camera_script.scale_factor * Time.deltaTime;
		transform.position = transform.position + movement;
	}

	void OnDisable()
	{
		GM.PreGameState -= PaddlePreGame;
		GM.LiveGameState -= PaddleLiveGame;
		GM.PauseGameState -= PaddlePauseGame;
	}
}
