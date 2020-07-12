using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine; using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour { 
    public void LoadMainMenu() {  
        Debug.Log("Main Menu button pressed.");
        SceneManager.LoadScene("MainMenu"); 
    } 
    public void LoadLevelSelect() {  
        Debug.Log("Level Select button pressed.");
        SceneManager.LoadScene("LevelSelect") ; 
    } 
    public void LoadHowToPlay() {  
        Debug.Log("How To Play button pressed.");
        SceneManager.LoadScene("HowToPlay") ; 
    } 
    public void LoadCredits() { 
        Debug.Log("Credits button pressed.");
        SceneManager.LoadScene("Credits") ; 
    } 
}
