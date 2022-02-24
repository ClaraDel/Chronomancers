using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int positionX;
    private int positionY;
    private int nbTiles;
    private int idxUp;
    private int idxDown;
    private int idxLeft;
    private int idxRight;
    private List<RedTilePopup> activeTiles;
    private List<RedTilePopup> effectTiles;
    Vector3 positionAfficheur;
    public bool hasValidated = false;
    public Vector3 posAfterReturn = new Vector3();

    private enum directions
    {
        right,
        up,
        left,
        down
    };


    private Dictionary<Vector3, RedTilePopup> travelArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void destroy()
    {
        Destroy(transform.gameObject);
    }

    public static CursorManager create(Vector3 position, List<RedTilePopup> activeTiles,
        int nb, List<RedTilePopup> effectTiles, Vector3 positionAfficheur)
    {
        Transform cursorManagerTransform = Instantiate(GameAssets.i.pfCursor, position, Quaternion.identity);
        CursorManager cursorManager = cursorManagerTransform.GetComponent<CursorManager>();
        cursorManager.setUp(activeTiles,nb,effectTiles, positionAfficheur);
        return cursorManager;
    }

    void setUp(List<RedTilePopup> activeTiles, int nb, List<RedTilePopup> effectTiles, Vector3 positionAfficheur)
    {
        travelArea = new Dictionary<Vector3, RedTilePopup>();
        for(int i = 0; i < activeTiles.Count; i++)
        {
            travelArea[activeTiles[i].transform.position] = activeTiles[i];
        }
        positionX = Mathf.RoundToInt(transform.position.x);
        positionY = Mathf.RoundToInt(transform.position.y);
        nbTiles = nb + 1;
        idxUp = 3 * nbTiles;
        idxLeft = 2 * nbTiles;
        idxDown =  nbTiles;
        idxRight = 0;
        this.activeTiles = activeTiles;
        this.effectTiles = effectTiles;
        this.positionAfficheur = positionAfficheur;
    }

    public Vector3 projectPosition(Vector3 position, float theta)
    {
        Vector3 projectedPosition;

        float tmp = (position.x) * Mathf.Cos(theta) - (position.y) * Mathf.Sin(theta);
        Vector3 pos = position;
        pos.y = Mathf.RoundToInt((position.x) * Mathf.Sin(theta) + (position.y) * Mathf.Cos(theta));
        pos.x = Mathf.RoundToInt(tmp);
        position = pos;
        projectedPosition = position;
        return projectedPosition;
    }

    public List<Vector3> projectPosition(List<Vector3> positions, float theta)
    {
        List<Vector3> projectedPositions;
        for (int i = 0; i < positions.Count; i++)
        {
            float tmp = (positions[i].x) * Mathf.Cos(theta) - (positions[i].y) * Mathf.Sin(theta);
            Vector3 pos = positions[i];
            pos.y = Mathf.RoundToInt((positions[i].x) * Mathf.Sin(theta) + (positions[i].y) * Mathf.Cos(theta));
            pos.x = Mathf.RoundToInt(tmp);
            positions[i] = pos;

        }
        projectedPositions = positions;
        return projectedPositions;

    }



    void updatePosCursor(ref int pos, int step, Vector3 translator, bool cond, int idx, int direction)
    {
        pos += step;
        Vector3 newPos = new Vector3(positionX, positionY, 0);
        if (travelArea.ContainsKey(newPos))
        {
            transform.Translate(translator);
            for (int i = 0; i < effectTiles.Count; i++)
            {
                effectTiles[i].transform.Translate(translator);
            }
        }
        else
        {
            int newPosy = (int)activeTiles[idx].transform.position.y;
            if (cond)
            {
                pos -= step;
            }
            else
            {
                
                int offset = this.getCurrentDirection(gameObject.transform.position, activeTiles) - direction;
             
                Vector3 newPosition = projectPosition(gameObject.transform.position - positionAfficheur, (Mathf.PI/2)*(-offset));
                for(int i = 0; i < effectTiles.Count; i++)
                {
                    Vector3 newPositionEffect = projectPosition(effectTiles[i].transform.position - positionAfficheur, (Mathf.PI / 2) * (-offset));
                    effectTiles[i].transform.position = newPositionEffect + positionAfficheur;
                }

                Vector3 tmpPosition = newPosition + positionAfficheur;
                positionX = Mathf.RoundToInt(tmpPosition.x);
                positionY = Mathf.RoundToInt(tmpPosition.y);
                gameObject.transform.position = new Vector3(positionX, positionY,0);

            }

        }

    }

    int getCurrentDirection(Vector3 position, List<RedTilePopup> tiles )
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if(tiles[i].transform.position == position)
            {
                if(i >= idxDown && i < idxLeft)
                {
                    return (int)directions.down;

                } else if (i >= idxLeft && i < idxUp)
                {
                    return (int)directions.left;
                } else if(i >= idxUp)
                {
                    return (int) directions.up; 
                } else
                {
                    return (int)directions.right;

                }
            }
        }
        return  -100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            updatePosCursor(ref positionY, 1, new Vector3(0, 1, 0),
                (int)activeTiles[idxUp].transform.position.y < positionY,idxUp,(int) directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            updatePosCursor(ref positionY, -1, new Vector3(0, -1, 0), 
                (int)activeTiles[idxDown].transform.position.y > positionY, idxDown, (int)directions.down);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            updatePosCursor(ref positionX, -1, new Vector3(-1, 0, 0), 
                (int)activeTiles[idxLeft].transform.position.x > positionX, idxLeft, (int)directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            updatePosCursor(ref positionX, 1, new Vector3(1, 0, 0), 
                (int)activeTiles[idxRight].transform.position.x < positionX, idxRight, (int)directions.right);

        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            
        }
    }

}
