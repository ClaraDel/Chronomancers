using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afficheur : MonoBehaviour
{

    private List<RedTilePopup> activeTiles;
    private List<RedTilePopup> effectTiles;
    private List<Vector3> zoneEffet;
    private Vector3 position;
    public CursorManager cursor;
    private int porteeMin;
    private int porteeMax;
    Vector3 cursorPosition;
    bool firstCreate = true;
    bool done = false;
    public bool isDisplaying = false;


    public static Afficheur create(Vector3 position, int porteeMin, int porteeMax, List<Vector3> zoneEffet)
    {
        Transform afficheurTransform = Instantiate(GameAssets.i.pfAfficheur, position, Quaternion.identity);
        Afficheur afficheur = afficheurTransform.GetComponent<Afficheur>();
        afficheur.setUp(position, porteeMin, porteeMax, zoneEffet);
        return afficheur;
    }

    public Vector3 getCursorPosition()
    {
        if (done)
        {
            return cursorPosition;
        } else
        {
            return new Vector3();
        }
    }

    public void setUp(Vector3 position, int porteeMin, int porteeMax, List<Vector3> zoneEffet)
    {
        this.position = position;
        this.porteeMax = porteeMax;
        this.porteeMin = porteeMin;
        this.zoneEffet = zoneEffet;
    }


    public List<Vector3> displayedPositions
    {
        get
        {
            List<Vector3> positions = new List<Vector3>();
            for(int i = 0; i <= porteeMax- porteeMin; i++)
            {
                positions.Add(new Vector3(porteeMin + i, 0, 0));
            }
            return positions;
        }
    }

    public void endDisplay()
    {
        int nb = activeTiles.Count;
        for (int i = 0; i < nb; i++)
        {
            RedTilePopup tmp = activeTiles[activeTiles.Count - 1];
            activeTiles.Remove(tmp);
            cursor.destroy();
            tmp.destroy(); ;
        }
        int nb1 = effectTiles.Count;
        for (int i = 0; i < nb1; i++)
        {
            RedTilePopup tmp = effectTiles[effectTiles.Count - 1];
            effectTiles.Remove(tmp);
            tmp.destroy(); 
        }
        isDisplaying = false;
    }

    public void display()
    {
        isDisplaying = true;
        activeTiles = new List<RedTilePopup>();
        createRedTiles(displayedPositions);
        for (int i = 1; i < 4; i++)
        {
            List<Vector3> projectedPositions = projectPosition(displayedPositions, (Mathf.PI / 2) * (-i));
            createRedTiles(projectedPositions);
        }
        createZoneEffet(zoneEffet);
        cursor = CursorManager.create(position + displayedPositions[0], activeTiles, porteeMax - porteeMin,effectTiles,position);
        if (cursor!= null)
        {
            cursorPosition = cursor.transform.position;
        }
    }


    public List<Vector3> projectPosition(List<Vector3> positions, float theta)
    {
        List<Vector3> projectedPositions;
        for (int i = 0; i < positions.Count; i++)
        {
            float tmp = (positions[i].x) * Mathf.Cos(theta) - (positions[i].y) * Mathf.Sin(theta);
            Vector3 pos = positions[i];
            pos.y = Mathf.RoundToInt((positions[i].x) * Mathf.Sin(theta) +(positions[i].y) * Mathf.Cos(theta));
            pos.x = Mathf.RoundToInt(tmp);
            positions[i] = pos;

        }
        projectedPositions = positions;
        return projectedPositions;

    }

    public void createZoneEffet(List<Vector3> zoneEffet)
    {
        effectTiles = new List<RedTilePopup>();
        for (int i = 0; i < zoneEffet.Count; i++)
        {
            RedTilePopup tmp = RedTilePopup.create(activeTiles[0].transform.position + zoneEffet[i]);
            tmp.gameObject.transform.Find("Square").gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            effectTiles.Add(tmp);
        }
    }

    public void createRedTiles(List<Vector3> projectedPositions)
    {
        for (int i = 0; i < displayedPositions.Count; i++)
        {
            RedTilePopup tmp = RedTilePopup.create(position + projectedPositions[i]);
            activeTiles.Add(tmp);
        }
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cursorPosition = cursor.transform.position;
            done = true;
            Debug.Log(cursorPosition);
        }

    }
}
