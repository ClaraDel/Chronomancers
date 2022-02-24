using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int positionX;
    private int positionY;

    private Dictionary<Vector3, RedTilePopup> travelArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void destroy()
    {
        Destroy(transform.gameObject);
    }

    public static CursorManager create(Vector3 position, List<RedTilePopup> activeTiles)
    {
        Transform cursorManagerTransform = Instantiate(GameAssets.i.pfCursor, position, Quaternion.identity);
        CursorManager cursorManager = cursorManagerTransform.GetComponent<CursorManager>();
        cursorManager.setUp(activeTiles);
        return cursorManager;
    }

    void setUp(List<RedTilePopup> activeTiles)
    {
        travelArea = new Dictionary<Vector3, RedTilePopup>();
        for(int i = 0; i < activeTiles.Count; i++)
        {
            travelArea[activeTiles[i].transform.position] = activeTiles[i];
        }
        positionX = Mathf.RoundToInt(transform.position.x);
        positionY = Mathf.RoundToInt(transform.position.y);
    }

    void setPosition(Vector2 pos)
    {
        positionX = (int) pos.x;
        positionY = (int) pos.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            positionY+=1;
            Vector3 newPos = new Vector3(positionX, positionY, 0);
            if(travelArea.ContainsKey(newPos))
            {
                transform.Translate(new Vector3(0, 1, 0));
            } else
            {
                positionY -= 1;
            }    
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            positionY-=1;
            Vector3 newPos = new Vector3(positionX, positionY, 0);
            if (travelArea.ContainsKey(newPos))
            {
                transform.Translate(new Vector3(0, -1, 0));
            }
            else
            {
                positionY += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            positionX-=1;
            Vector3 newPos = new Vector3(positionX, positionY, 0);
            if (travelArea.ContainsKey(newPos))
            {
                transform.Translate(new Vector3(-1, 0, 0));
            }
            else
            {
                positionX += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            positionX += 1;
            Vector3 newPos = new Vector3(positionX, positionY, 0);
            if (travelArea.ContainsKey(newPos))
            {
                transform.Translate(new Vector3(1, 0, 0));
            }
            else
            {
                positionX -= 1;
            }
        }
    }

}
