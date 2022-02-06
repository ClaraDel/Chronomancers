using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("START");
    } 
    
    public void StartTutorial()
    {
        Debug.Log("TUTORIAL");
    } 
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
