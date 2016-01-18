using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [SerializeField]
	private float ball_speed = 0f;

	private float x = 1f;
	private float y = 1f;
	
	private GameManager GM;
	private GameObject paddle;
	private CameraSetup camera_script;
	private Vector3 ball_vector;
	private Vector3 direction;
	private Vector3? pause_vector;
	private Vector3 reflect_direction;
	private Rigidbody2D rb;
	private AudioSource bounce_sound;
    private float paddle_bounce_offset;
	
	void OnEnable()
	{
		GM = GameManager.instance;
		GM.PreGameState += BallPreGame;
		GM.LiveGameState += BallLiveGame;
		GM.PauseGameState += BallPauseGame;
	}

	void Awake()
	{
		camera_script = GameObject.FindGameObjectWithTag(Tags.main_cam).GetComponent<CameraSetup>();
		paddle = GameObject.FindGameObjectWithTag(Tags.paddle);
		ball_vector = new Vector3(x, y, 0f);
		rb = GetComponent<Rigidbody2D>();
		bounce_sound = GetComponent<AudioSource>();
	}
	
	void BallPreGame ()
	{
		// Stuff that happens to the ball in pregamestate
	}
	
	void Update()
	{
		if (GM.game_state == GameState.Pre_Game)
		{
			// If pre_game, movement same as paddle
			transform.position = paddle.transform.position + new Vector3(0f, camera_script.brick_height, 0f);
			
			// On left click, set velocity, call set_to_live
			if (Input.GetButtonUp("Launch"))
			{
				SetDirection(ball_vector);
				GM.SetToLiveGame();
			}
		}
        else if (GM.game_state == GameState.Live_Game)
        {
            SetDirection(direction);
        }
	}
	
	void BallLiveGame()
	{
        if (pause_vector != null)
        {
            SetDirection(pause_vector.Value);
            pause_vector = null;
        }
	}
	
	void BallPauseGame()
	{
        pause_vector = direction;
        SetDirection(new Vector3(0f, 0f, 0f));
	}
	
	void SetDirection(Vector3 in_direction)
	{
		direction = in_direction;
		direction.Normalize();
		//rb.velocity = direction * ball_speed * Time.deltaTime; // old way of moving ball
        transform.position = transform.position + (direction * ball_speed * camera_script.scale_factor * Time.deltaTime);
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
        if (coll.gameObject.tag == Tags.floor)
        {
            GM.LoseLife();
        }

		if (coll.gameObject.tag == Tags.brick && bounce_sound.isPlaying) { bounce_sound.Stop(); }
		rb.velocity = Vector2.zero;
		reflect_direction = Vector3.Reflect(direction, coll.contacts[0].normal);
		if (coll.gameObject.tag == Tags.paddle)
		{
            // Calculates how far from center the ball hit and tweaks the angle accordingly
            // TODO: this works but very strangely, look into rework
            paddle_bounce_offset = transform.position.x - coll.gameObject.transform.position.x;
            paddle_bounce_offset = paddle_bounce_offset / (coll.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
            reflect_direction.x += paddle_bounce_offset;
		}
		SetDirection(reflect_direction);
		if (coll.gameObject.tag == Tags.brick) { bounce_sound.Play(); }
		
	}
	
	void OnDisable()
	{
		GM.PreGameState -= BallPreGame;
		GM.LiveGameState -= BallLiveGame;
		GM.PauseGameState -= BallPauseGame;
	}
}