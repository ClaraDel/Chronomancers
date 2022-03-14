using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audiomixer;

    public void StartGame()
    {
        Debug.Log("START");
        SceneManager.LoadScene("MainScene");
    } 
    
    public void StartTutorial()
    {
        Debug.Log("TUTORIAL");
        SceneManager.LoadScene("MainScene");
    } 

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetMainVolume(float volume)
    {
        audiomixer.SetFloat("MainVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFXVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audiomixer.SetFloat("MusicVolume", volume);
    }



    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
