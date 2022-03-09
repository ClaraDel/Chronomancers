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
        if (!fadingIn) return;

        if (UIGroup.alpha >= 1) return;

        UIGroup.alpha += Time.deltaTime;
        if (UIGroup.alpha >= 1)
        {
            fadingIn = false;
            field.interactable = true;
        }
    }
}
