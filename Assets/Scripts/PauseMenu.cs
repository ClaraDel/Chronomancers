using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;

    public void ContinueGame()
    {
        pauseMenu.alpha = 0.0f;
        pauseMenu.blocksRaycasts = false;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
