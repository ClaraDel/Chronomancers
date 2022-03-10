using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIGroup;
    [SerializeField] private bool fadingIn = false;
    [SerializeField] private Button field;

    public void fadeIn()
    {
        fadingIn = true;
    }

    void Update()
    {
        if (UIGroup.alpha >= 1) return;

        if (!fadingIn)
        {
            if (Input.GetKeyDown("space"))
            {
                UIGroup.alpha = 0;
                fadingIn = true;
            }
            return;
        }

        UIGroup.alpha += Time.deltaTime;
        if (UIGroup.alpha >= 1)
        {
            fadingIn = false;
            field.interactable = true;
        }
    }
}
