using System.Collections;
using System.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public Hashtable worldSpace;

    public void UpdateTable(){
        GameObject[] characters = GameObject.FindGameObjectsWithTag("character");
        foreach (var character in characters)
        {
            worldSpace.Add(new Vector2(character.transform.position.x, character.transform.position.y), character); //A remplacer par positionX, positionY de Character
        }
    }

}