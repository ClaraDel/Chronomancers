using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static GameObject selected;
    public static bool mouseOnObject = false;


    public static void updateStatePreviousButton(Color normalColor)
    {
        selected.GetComponent<CustomCharacterButton>().pressed = false;
        selected.GetComponent<CustomCharacterButton>().resetButton();
        selected = null;
    }



    public static void setSelected(GameObject go)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !mouseOnObject)
        {
            if(selected != null)
            {
                selected.transform.GetComponent<CustomCharacterButton>().hideUI();
                selected.GetComponent<CustomCharacterButton>().pressed = false;
                selected.GetComponent<CustomCharacterButton>().resetButton();
                selected = null;
            }
        }
    }
}
