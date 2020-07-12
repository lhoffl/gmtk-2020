using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine; using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour { 
    public void LoadMainMenu() {  SceneManager.LoadScene("MainMenu") ; } 
    public void LoadLevelSelect() {  SceneManager.LoadScene("LevelSelect") ; } 
    public void LoadHowToPlay() {  SceneManager.LoadScene("HowToPlay") ; } 
    public void LoadCredits() {  SceneManager.LoadScene("Credits") ; } 
}
