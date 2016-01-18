using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	
	// Handles splash screen 
	public float splash_delay = 4f;

	private GameManager GM;
	private Animator anim;

	void Awake ()
	{
		GM = GameManager.instance;
		anim = GetComponent<Animator>();
		StartCoroutine("DelayAndLaunchScene");	
	}

	IEnumerator DelayAndLaunchScene()
	{
		anim.Play("SplashScreen");
		yield return new WaitForSeconds(splash_delay);
		NextScene();
		
	}
	
	void NextScene()
	{
		StopCoroutine("DelayAndLaunchScene");
		GM.SetToMenu();
	}
}