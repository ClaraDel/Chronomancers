using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseToggle : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.alpha = (pauseMenu.alpha == 0.0f)? 1.0f : 0.0f;
            pauseMenu.blocksRaycasts = (pauseMenu.alpha == 0.0f)? false : true;
        }
    }

    public bool getIfPaused()
    {
        return pauseMenu.alpha == 1.0f;
    }
}
