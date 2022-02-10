using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private int positionX;
    private int positionY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            positionY+=1;
            transform.Translate(new Vector3(0,1,0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            positionY-=1;
            transform.Translate(new Vector3(0,-1,0));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            positionX-=1;
            transform.Translate(new Vector3(-1,0,0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            positionX+=1;
            transform.Translate(new Vector3(1,0,0));
        }
    }

}
