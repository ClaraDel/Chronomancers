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
    private bool validPosition = true;

    Vector3 positionAfficheur;

    Zone zone;
    enum directions
    {
        up,
        left,
        down,
        right
    }
    int direction = (int)directions.right;

    private Dictionary<Vector3, RedTilePopup> travelArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void destroy()
    {
        Destroy(transform.gameObject);
    }

    public static CursorManager create(List<RedTilePopup> activeTiles,
        int nb, List<RedTilePopup> effectTiles, Vector3 positionAfficheur, Zone zone)
    {
        Vector3 position = activeTiles[0].transform.position;
        Transform cursorManagerTransform = Instantiate(GameAssets.i.pfCursor, position, Quaternion.identity);
        CursorManager cursorManager = cursorManagerTransform.GetComponent<CursorManager>();
        cursorManager.setUp(activeTiles, nb, effectTiles, positionAfficheur, zone);
        return cursorManager;
    }

    void setUp(List<RedTilePopup> activeTiles, int nb, List<RedTilePopup> effectTiles, Vector3 positionAfficheur, Zone zone)
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
        this.positionAfficheur = positionAfficheur;
        this.zone = zone;
        adaptPosEffects();
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

    public void rotateEffects(float theta)
    {
        List<Vector3> newZoneEffets = new List<Vector3>() ;
        for (int i = 0; i < effectTiles.Count; i++)
        {
            Vector3 newPositionEffect = project1Position(effectTiles[i].transform.position - gameObject.transform.position, theta);
            effectTiles[i].transform.position = newPositionEffect + gameObject.transform.position;
            newZoneEffets.Add(newPositionEffect);
        }
        zone.setZoneEffet(newZoneEffets);
    }

    public void adaptPosEffects()
    {
        if (validPosition && effectTiles != null)
        {

            int posXRelatif = Mathf.RoundToInt(positionX - positionAfficheur.x);
            int posYRelatif = Mathf.RoundToInt(positionY - positionAfficheur.y);
            int offset = 0;

            if (posXRelatif == 0 && posYRelatif < 0)
            {
                offset = direction - (int)directions.down;
                direction = (int)directions.down;
            }
            else if (posXRelatif == 0 && posYRelatif > 0)
            {
                offset = direction - (int)directions.up;
                direction = (int)directions.up;

            }
            else if (posXRelatif < 0)
            {
                offset = direction - (int)directions.left;
                direction = (int)directions.left;
            }
            else
            {
                offset = direction - (int)directions.right;
                direction = (int)directions.right;
            }
            rotateEffects(-offset * (Mathf.PI / 2));
        }
    }

    void updatePosCursor(ref int pos, int step, Vector3 translator)
    {
        pos += step;
        Vector3 newPos = new Vector3(positionX, positionY, 0);
        if (travelArea.ContainsKey(newPos) || (nbTiles == 1 && effectTiles != null))
        {
            transform.Translate(translator);
            int n = effectTiles == null ? 0 : effectTiles.Count;

            for (int i = 0; i < n; i++)
            {
                effectTiles[i].transform.position += (translator);
                

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
            adaptPosEffects();
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