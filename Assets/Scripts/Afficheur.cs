using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afficheur : MonoBehaviour
{

    private List<RedTilePopup> activeTiles;
    private List<RedTilePopup> effectTiles;

    private List<Vector3> positionTiles;
    private List<Vector3> zoneEffet;

    private Vector3 position;
    public CursorManager cursor;

    private int porteeMin;
    private int porteeMax;

    Vector3 cursorPosition;
    public bool isDisplaying = false;

    private Zone zone;

    /*************** create *************/
    public static Afficheur create(Vector3 position, int porteeMin, int porteeMax, List<Vector3> zoneEffet)
    {
        Transform afficheurTransform = Instantiate(GameAssets.i.pfAfficheur, position, Quaternion.identity);
        Afficheur afficheur = afficheurTransform.GetComponent<Afficheur>();
        afficheur.setUp(position, porteeMin, porteeMax, zoneEffet);
        return afficheur;
    }

    public static Afficheur create(Vector3 position, List<Vector3> positionTiles)
    {
        Transform afficheurTransform = Instantiate(GameAssets.i.pfAfficheur, position, Quaternion.identity);
        Afficheur afficheur = afficheurTransform.GetComponent<Afficheur>();
        afficheur.setUp(position, positionTiles);
        return afficheur;
    }

    public static Afficheur create(Zone zone)
    {
        Transform afficheurTransform = Instantiate(GameAssets.i.pfAfficheur, zone.getPosition(), Quaternion.identity);
        Afficheur afficheur = afficheurTransform.GetComponent<Afficheur>();
        afficheur.setUp(zone); 
        return afficheur;
    }


    /*************** setUp *************/
    private void setUp(Vector3 position, List<Vector3> positionTiles)
    {
        this.position = position;
        this.positionTiles = positionTiles;
    }

    private void setUp(Zone zone)
    {
        this.position = zone.getPosition();
        this.porteeMax = zone.getPorteeMax();
        this.porteeMin = zone.getPorteeMin();
        this.zoneEffet = zone.getZoneEffets();
        this.zone = zone;
    }

    private void setUp(Vector3 position, int porteeMin, int porteeMax, List<Vector3> zoneEffet)
    {
        this.position = position;
        this.porteeMax = porteeMax;
        this.porteeMin = porteeMin;
        this.zoneEffet = zoneEffet;
    }

    public Vector3 getCursorPosition()
    {
        return cursor.transform.position;
    }

    private List<Vector3> buildLosange(int range)
    {
        List<Vector3> positions = new List<Vector3>();
        int y = range;
        int x = 0;
        int s = 1;
        positions.Add(new Vector3(0, y, 0));
        for (int i = range - 1; i > -range; i--)
        {
            if (i == -1)
            {
                s = -1;
            }
            x += s;
            Vector3 positionTile = new Vector3(x, i, 0);
            Vector3 projectedPosition = project1Position(positionTile, Mathf.PI);
            positions.Add(positionTile);
            positions.Add(projectedPosition);
        }
        positions.Add(new Vector3(0, -y, 0));
        return positions;
    }

    private void buildLosanges()
    {
        for (int i = porteeMin; i <= porteeMax; i++)
        {
            createRedTiles(buildLosange(i));

        }
    }

    public void rotateEffects()
    {
        cursor.rotateEffects(Mathf.PI/2);
    }

    private List<Vector3> projectPosition(List<Vector3> positions, float theta)
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

    private Vector3 project1Position(Vector3 position, float theta)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(position);
        return projectPosition(positions, theta)[0];
    }

    private void createZoneEffet(List<Vector3> zoneEffet)
    {
        effectTiles = new List<RedTilePopup>();
        for (int i = 0; i < zoneEffet.Count; i++)
        {
            RedTilePopup tmp = RedTilePopup.create(activeTiles[0].transform.position + zoneEffet[i]);
            tmp.gameObject.transform.Find("Square").gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            effectTiles.Add(tmp);
        }
    }

    private void createRedTiles(List<Vector3> projectedPositions)
    {
        for (int i = 0; i < projectedPositions.Count; i++)
        {
            RedTilePopup tmp = RedTilePopup.create(position + projectedPositions[i]);
            activeTiles.Add(tmp);
        }
    }

    public void display()
    {
        if (!isDisplaying)
        {
            isDisplaying = true;
            activeTiles = new List<RedTilePopup>();
            if (positionTiles == null)
            {
                buildLosanges();
                createZoneEffet(zoneEffet);
            }
            else
            {
                createRedTiles(positionTiles);
            }
            cursor = CursorManager.create(activeTiles, porteeMax - porteeMin, effectTiles, position, zone);
            if (cursor != null)
            {
                cursorPosition = cursor.transform.position;
            }
        }
    }

    public void endDisplay()
    {
        if (isDisplaying)
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
            Destroy(gameObject);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cursor != null && Input.GetKeyDown(KeyCode.J))
        {
            cursor.rotateEffects(Mathf.PI/2);
        }
    }
}