using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private Vector3 origin; // Centre de la zone où le curseur peut se déplacer
    private GameObject zoneCiblable;
    private GameObject zoneEffets;
    private List<GameObject> tilesCiblable;
    private List<GameObject> tilesEffets;
    public void init(Vector3 origin, GameObject zoneCiblable, GameObject zoneEffets){
        this.origin = origin;
        tilesCiblable = new List<GameObject>();
        tilesEffets = new List<GameObject>();
        this.zoneCiblable = zoneCiblable;
        this.zoneEffets = zoneEffets;
        foreach (Transform tile in zoneCiblable.transform)
        {
            tilesCiblable.Add(tile.gameObject);
        }
        foreach (Transform tile in zoneEffets.transform)
        {
            tilesEffets.Add(tile.gameObject);
        }
    }

    public Vector3 getorigin()
    {
        return origin;
    }

    public List<GameObject> getTilesEffets()
    {
        return tilesEffets;
    }

    public void setTilesEffet(List<GameObject> tilesEffets)
    {
        this.tilesEffets = tilesEffets;
    }

    public List<GameObject> getTilesCiblable()
    {
        return tilesCiblable;
    }

    public void setTilesCiblable(List<GameObject> tilesCiblable)
    {
        this.tilesCiblable = tilesCiblable;
    }

    public GameObject getZoneCiblable(){
        return this.zoneCiblable;
    }
    public GameObject getZoneEffet(){
        return this.zoneEffets;
    }

}
