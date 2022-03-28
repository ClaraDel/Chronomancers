using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : Info
{
    GameObject characterPrefab;
    int posX;
    int posY;

    public override void addInfo(Info info)
    {
        SpawnInfo spawnInfo = (SpawnInfo)info;
        characterPrefab = spawnInfo.characterPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
