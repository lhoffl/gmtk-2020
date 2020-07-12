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
    public void LoadLevel1() { 
        Debug.Log("Level1 button pressed.");
        SceneManager.LoadScene("Level1-TwoVsOne") ; 
    }
    public void LoadLevel2() { 
        Debug.Log("Level2 button pressed.");
        SceneManager.LoadScene("Level2-TwoVsTwo") ; 
    }
    public void LoadLevel3() { 
        Debug.Log("Level3 button pressed.");
        SceneManager.LoadScene("Level3-Lanes") ; 
    }
    public void LoadLevel4() { 
        Debug.Log("Level4 button pressed.");
        SceneManager.LoadScene("Level4-StartAndStop") ; 
    }
    public void LoadLevel5() { 
        Debug.Log("Level5 button pressed.");
        SceneManager.LoadScene("Level5-FireHell") ; 
    }
}
