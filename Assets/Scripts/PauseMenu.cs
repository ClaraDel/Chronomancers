using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;

    public void ContinueGame()
    {
        pauseMenu.enabled = false;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
