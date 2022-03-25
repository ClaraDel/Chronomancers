using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIGroup;
    [SerializeField] private bool fadingIn = false;
    [SerializeField] private Button field;
    [SerializeField] private WinMenu winMenu;

    public void fadeIn()
    {
        fadingIn = true;
        UIGroup.alpha = 0;
        Debug.Log(winMenu);
        winMenu.ResultUI();
    }

    void Update()
    {
        if (UIGroup.alpha >= 1) return;

        if (!fadingIn)
        {
            return;
        }

        UIGroup.alpha += Time.deltaTime;
        if (UIGroup.alpha >= 1)
        {
            fadingIn = false;
            UIGroup.blocksRaycasts = true;
            field.interactable = true;
        }
    }
}
