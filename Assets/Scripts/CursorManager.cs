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
            Vector3 tmp = tile.transform.position;
            tmp.x = (int)tmp.x;
            tmp.y = (int)tmp.y;
            tmp.z = (int)tmp.z;
            listPositionsActif.Add(tmp);
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            updatePosCursor(new Vector3(this.positionX, this.positionY + 1, 0), directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            updatePosCursor(new Vector3(this.positionX, this.positionY - 1, 0), directions.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            updatePosCursor(new Vector3(this.positionX - 1, this.positionY, 0), directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            updatePosCursor(new Vector3(this.positionX + 1, this.positionY, 0), directions.right);
        }
    }

}
