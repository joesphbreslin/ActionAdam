using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD;
using FMODUnity;

public class MainMenu : MonoBehaviour 
{
    static public StudioEventEmitter SEEE;
    
	public void PlayGame(string sceneName)
	{
        SEEE = GetComponent<StudioEventEmitter>();
        SEEE.Play();
     
		SceneManager.LoadScene(sceneName);
	}
	public void QuitGame()
	{
        SEEE = GetComponent<StudioEventEmitter>();
        SEEE.Play();
        Application.Quit();
	}


}
