using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int positionX;
    private int positionY;
    private int nbTiles;
    private List<RedTilePopup> activeTiles;
    private List<RedTilePopup> effectTiles;
    public bool hasValidated = false;
    public Vector3 posAfterReturn = new Vector3();
    private bool validPosition = true;

    


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
        cursorManager.setUp(activeTiles, nb, effectTiles, positionAfficheur);
        return cursorManager;
    }

    void setUp(List<RedTilePopup> activeTiles, int nb, List<RedTilePopup> effectTiles, Vector3 positionAfficheur)
    {
        travelArea = new Dictionary<Vector3, RedTilePopup>();
        for (int i = 0; i < activeTiles.Count; i++)
        {
            travelArea[activeTiles[i].transform.position] = activeTiles[i];
        }
        positionX = Mathf.RoundToInt(transform.position.x);
        positionY = Mathf.RoundToInt(transform.position.y);
        nbTiles = nb + 1;
        this.activeTiles = activeTiles;
        this.effectTiles = effectTiles;
    }







    void updatePosCursor(ref int pos, int step, Vector3 translator)
    {
        pos += step;
        Vector3 newPos = new Vector3(positionX, positionY, 0);
        if (travelArea.ContainsKey(newPos) || nbTiles == 1)
        {
            transform.Translate(translator);
            for (int i = 0; i < effectTiles.Count; i++)
            {
                effectTiles[i].transform.Translate(translator);
                if (!travelArea.ContainsKey(newPos))
                {
                    effectTiles[i].gameObject.SetActive(false);
                    validPosition = false;
                }
                else
                {
                    effectTiles[i].gameObject.SetActive(true);
                    validPosition = true;
                }
            }
        }
        else
        {
            pos -= step;
        }

    }

    public bool isValidPosition()
    {
        return validPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            updatePosCursor(ref positionY, 1, new Vector3(0, 1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            updatePosCursor(ref positionY, -1, new Vector3(0, -1, 0));

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            updatePosCursor(ref positionX, -1, new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            updatePosCursor(ref positionX, 1, new Vector3(1, 0, 0));

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

}