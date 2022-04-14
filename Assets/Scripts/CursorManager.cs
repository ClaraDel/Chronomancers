using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int positionX;
    private int positionY;
    public enum directions
    {
        up,
        left,
        down,
        right
    }
    public directions direction = directions.up;

    public Zone activeZone;
    private List<Vector3Int> listPositionsActif;
    private bool rotationActive;
    private bool rotateZone;

    public void setUp(Zone zone)
    {
        rotationActive = false;
        zone.getZoneCiblable().SetActive(true);
        listPositionsActif = new List<Vector3Int>();
        foreach (var tile in zone.getTilesCiblable())
        {
            Vector3Int tmp = new Vector3Int(0,0,0);
            tmp.x = (int)Mathf.RoundToInt(tile.transform.position.x);
            tmp.y = (int)Mathf.RoundToInt(tile.transform.position.y);
            tmp.z = (int)Mathf.RoundToInt(tile.transform.position.z);
            listPositionsActif.Add(tmp);
        }
        this.activeZone = zone;
        transform.position = listPositionsActif[0];
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        activeZone.getZoneEffet().SetActive(true);
    }

    public void setUpRotation(Zone zone)
    {
        rotationActive = true;
        direction = directions.up;
        zone.getZoneCiblable().SetActive(true);
        listPositionsActif = new List<Vector3Int>();
        foreach (var tile in zone.getTilesCiblable())
        {
            Vector3Int tmp = new Vector3Int(0, 0, 0);
            tmp.x = (int)Mathf.RoundToInt(tile.transform.position.x);
            tmp.y = (int)Mathf.RoundToInt(tile.transform.position.y);
            tmp.z = (int)Mathf.RoundToInt(tile.transform.position.z);
            listPositionsActif.Add(tmp);
        }
        this.activeZone = zone;
        transform.position = listPositionsActif[0];
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        activeZone.getZoneEffet().SetActive(true);
        activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(0, Vector3Int.forward);
    }

    public void setUpFirewall(Zone zone)
    {
        rotateZone = true;
        direction = directions.up;
        zone.getZoneCiblable().SetActive(true);
        listPositionsActif = new List<Vector3Int>();
        foreach (var tile in zone.getTilesCiblable())
        {
            Vector3Int tmp = new Vector3Int(0, 0, 0);
            tmp.x = (int)Mathf.RoundToInt(tile.transform.position.x);
            tmp.y = (int)Mathf.RoundToInt(tile.transform.position.y);
            tmp.z = (int)Mathf.RoundToInt(tile.transform.position.z);
            listPositionsActif.Add(tmp);
        }
        this.activeZone = zone;
        transform.position = listPositionsActif[0];
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        activeZone.getZoneEffet().SetActive(true);
        activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(0, Vector3Int.forward);
    }

    public void reset()
    {
        if (activeZone != null)
        {
        this.activeZone.getZoneEffet().SetActive(false);
        this.activeZone.getZoneCiblable().SetActive(false);
        }
        listPositionsActif = new List<Vector3Int>();
        this.activeZone = null;
        direction = directions.up;
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
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(0, Vector3Int.forward);
                break;
            case directions.right:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(270, Vector3Int.forward);
                break;
            case directions.down:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(180, Vector3Int.forward);
                break;
            case directions.left:
                activeZone.getZoneEffet().transform.rotation = Quaternion.AngleAxis(90, Vector3Int.forward);
                break;
        }
    }

    public void rotateZoneEffect()
    {
        if (rotateZone && activeZone != null)
        {
            activeZone.getZoneEffet().transform.Rotate(new Vector3(0,0,90));
        }
    }

    bool updatePosCursor(Vector3Int new_position, directions new_direction)
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
    bool changeCursorPosition(Vector3Int new_position)
    {
        this.transform.position = new_position;
        this.positionX = (int)Mathf.Floor(new_position.x);
        this.positionY = (int)Mathf.Floor(new_position.y);
        this.calculOrientationCursor();
        if (rotationActive)
        {
            this.rotateEffects();
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            updatePosCursor(new Vector3Int(this.positionX, this.positionY + 1, 0), directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            updatePosCursor(new Vector3Int(this.positionX, this.positionY - 1, 0), directions.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            updatePosCursor(new Vector3Int(this.positionX - 1, this.positionY, 0), directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            updatePosCursor(new Vector3Int(this.positionX + 1, this.positionY, 0), directions.right);
        }
    }

    public int getPositionX()
    {
        return this.positionX;
    }

    public int getPositionY()
    {
        return this.positionY;
    }

}
