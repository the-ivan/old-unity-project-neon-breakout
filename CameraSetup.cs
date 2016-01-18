using UnityEngine;
using System.Collections;

public class CameraSetup : MonoBehaviour {
	// Sets camera position and orthographic size 
	// Also sets brick size and returns scale_factor for sprites

	private Camera main_cam;
	private int current_width;
	public float scale_factor {get; private set;}
	public float brick_height {get; private set;}
	public float brick_width  {get; private set;}
	
	void Awake () 
	{
        DontDestroyOnLoad(this);
		main_cam = GetComponent<Camera>();
		current_width = Screen.width;
		CameraWork();
        
	}

	void Update () 
	{
		if (Screen.width != current_width)
		{
			CameraWork();
		}
	}
	
	void CameraWork()
	{
		main_cam.orthographicSize = Screen.height / 2;
		transform.position = new Vector3 (Screen.width / 2,Screen.height / 2, -1);
		scale_factor = Screen.height / 1080f;
		brick_width = Screen.width * 3 / 80;
		brick_height = brick_width / 2;
	}
}