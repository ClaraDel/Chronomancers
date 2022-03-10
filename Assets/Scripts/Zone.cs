using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{

    private int porteeMax;
    private int porteeMin;
    private Vector3 position;
    private List<Vector3> zoneEffets;

    public Zone(Vector3 position, int porteeMin, int porteeMax, List <Vector3> zoneEffets)
    {
        this.porteeMax = porteeMax;
        this.porteeMin = porteeMin;
        this.position = position;
        this.zoneEffets = zoneEffets;
    }

    public Vector3 getPosition()
    {
        return position;
    }

    public List<Vector3> getZoneEffets()
    {
        return zoneEffets;
    }

    public int getPorteeMax()
    {
        return porteeMax;
    }

    public int getPorteeMin()
    {
        return porteeMin;
    }

    public void setZoneEffet(List<Vector3> zoneEffets)
    {
        this.zoneEffets = zoneEffets;
    }

}
