using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour {

	[Tooltip("_sceneToLoadOnPlay is the name of the scene that will be loaded when users click play")]
	public string _sceneToLoadOnPlay = "Level";
	[Tooltip("_webpageURL defines the URL that will be opened when users click on your branding icon")]
	public string _webpageURL = "http://google.com";
	
	//The private variable 'scene' defined below is used for example/development purposes.
	//It is used in correlation with the Escape_Menu script to return to last scene on key press.
	UnityEngine.SceneManagement.Scene scene;

	void Awake () {
		scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
	}
	
	public void OpenWebpage () {
		Application.OpenURL(_webpageURL);
	}
	
	public void PlayGame () {
		AudioSystem.Instance.PlaySFX("button");
		SceneSwitcher.Instance.ChangeScene(_sceneToLoadOnPlay);
	}
	public void OpenOptions() => VolumeSettings.Instance.Open();
	public void QuitGame () {
		AudioSystem.Instance.PlaySFX("button");
		#if !UNITY_EDITOR
			Application.Quit();
		#endif
		
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
