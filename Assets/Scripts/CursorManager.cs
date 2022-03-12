using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int positionX;
    private int positionY;
    enum directions
    {
        up,
        left,
        down,
        right
    }
    directions direction = directions.up;

    private Zone activeZone;
    private List<Vector3> listPositionsActif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setUp(Zone zone)
    {
        zone.getZoneCiblable().SetActive(true);
        listPositionsActif = new List<Vector3>();
        foreach (var tile in zone.getTilesCiblable())
        {
            listPositionsActif.Add(tile.transform.position);
        }
        this.activeZone = zone;
        transform.position = listPositionsActif[0];
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        activeZone.getZoneEffet().SetActive(true);
    }

    private void calculOrientationCursor()
    {
        float positionXrelative = positionX - this.transform.parent.position.x;
        float positionYrelative = positionY - this.transform.parent.position.y;
        if (positionXrelative <= positionYrelative && positionXrelative > -positionYrelative)
            this.direction = directions.up;
        else if (positionXrelative > positionYrelative && positionXrelative >= -positionYrelative)
            this.direction = directions.right;
        else if (positionXrelative < -positionYrelative && positionXrelative >= positionYrelative)
            this.direction = directions.down;
        else if (positionXrelative <= -positionYrelative && positionXrelative < positionYrelative)
            this.direction = directions.left;
    }

    public void rotateEffects()
    {
        switch (this.direction)
        {
            case directions.up:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                break;
            case directions.right:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                break;
            case directions.down:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;
            case directions.left:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;
        }
    }
    bool updatePosCursor(Vector3 new_position, directions new_direction)
    {
        if (new_position.magnitude<1f)
        {
            switch (new_direction)
            {
                case directions.up:
                    new_position.y += 1;
                    return updatePosCursor(new_position, directions.up);
                case directions.right:
                    new_position.x += 1;
                    return updatePosCursor(new_position, directions.right);
                case directions.down:
                    new_position.y -= 1;
                    return updatePosCursor(new_position, directions.down);
                case directions.left:
                    new_position.x -= 1;
                    return updatePosCursor(new_position, directions.left);
                default:
                    return false;
            }
        }
        if (this.listPositionsActif.Contains(new_position))
        {
            return changeCursorPosition(new_position);
        }
        else
        {
            switch (new_direction)
            {
                case directions.up:
                    new_position.x += 1;
                    break;
                case directions.right:
                    new_position.y -= 1;
                    break;
                case directions.down:
                    new_position.x -= 1;
                    break;
                case directions.left:
                    new_position.y += 1;
                    break;
            }
            if (this.listPositionsActif.Contains(new_position))
            {
                return changeCursorPosition(new_position);
            }
            else
            {
                switch (new_direction)
                {
                    case directions.up:
                        new_position.x -= 2;
                        break;
                    case directions.right:
                        new_position.y += 2;
                        break;
                    case directions.down:
                        new_position.x += 2;
                        break;
                    case directions.left:
                        new_position.y -= 2;
                        break;
                }
                if (this.listPositionsActif.Contains(new_position))
                {
                    return changeCursorPosition(new_position);
                }
            }
            return false;
        }

    }
    bool changeCursorPosition(Vector3 new_position)
    {
        this.transform.position = new_position;
        this.positionX = (int)Mathf.Floor(new_position.x);
        this.positionY = (int)Mathf.Floor(new_position.y);
        this.calculOrientationCursor();
        this.rotateEffects();
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        List<Vector3> projectedPositions;
        for (int i = 0; i < positions.Count; i++)
        {
            updatePosCursor(new Vector3(this.positionX, this.positionY + 1, 0), directions.up);
        }
        projectedPositions = positions;
        return projectedPositions;

    }



    void updatePosCursor(ref int pos, int step, Vector3 translator, bool cond, int idx, int direction)
    {
        pos += step;
        Vector3 newPos = new Vector3(positionX, positionY, 0);
        if (travelArea.ContainsKey(newPos) || nbTiles == 1)
        {
            updatePosCursor(new Vector3(this.positionX, this.positionY - 1, 0), directions.down);
        }
        else
        {
            updatePosCursor(new Vector3(this.positionX - 1, this.positionY, 0), directions.left);
        }

    }

    int getCurrentDirection(Vector3 position, List<RedTilePopup> tiles )
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            updatePosCursor(new Vector3(this.positionX + 1, this.positionY, 0), directions.right);
        }
        //should never get there
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
