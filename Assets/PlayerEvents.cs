using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
}
