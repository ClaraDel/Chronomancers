using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelected : MonoBehaviour
{
    bool clicked = false;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    void Update()
    {
        if (clicked) return;

        if (Input.anyKey)
        {
            button.Select();
            clicked = true;
        }
    }
}
