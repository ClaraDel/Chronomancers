using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public AudioSource sound;
    public AudioClip soundHover;
    public AudioClip soundConfirm;
    public AudioClip soundBack;

    public void playSoundHover()
    {
        sound.PlayOneShot(soundHover);
    }
    public void playSoundConfirm()
    {
        sound.PlayOneShot(soundConfirm);
    }
    public void playSoundBack()
    {
        sound.PlayOneShot(soundBack);
    }
    public void playSoundStart()
    {
        sound.Play();
    }

    public void StartGame()
    {
        Debug.Log("START");
        SceneManager.LoadScene("MainScene");
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetMainVolume(float volume)
    {
        audiomixer.SetFloat("MainVolume", volume / 2);
    }
    public void SetSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFXVolume", volume / 2);
    }
    public void SetMusicVolume(float volume)
    {
        audiomixer.SetFloat("MusicVolume", volume / 2);
    }



    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
