using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseToggle : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.enabled = !pauseMenu.enabled;
        }
    }
}
