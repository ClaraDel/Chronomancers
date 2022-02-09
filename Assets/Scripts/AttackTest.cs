using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : Attack
{
    public RedTilePopup[] tiles = new RedTilePopup [4];
    public RedTilePopup activeTile;
    public int damage
    {
        get
        {
            return 50;
        }
    }

    public Vector3[] possibleAttackPositions { 
        get {
            return new [] {
                new Vector3 { x = 0.5f, y = 1.5f,z = 0 },
                new Vector3 { x = 0.5f, y = -0.5f,z = 0 },
                new Vector3 { x = -0.5f, y = 0.5f,z = 0 },
                new Vector3 { x= 1.5f, y= 0.5f, z = 0 } 
            };
        }
    }

    public void selectAttack(string direction)
    {
        Debug.Log(direction);
        if(tiles.Length == 0)
        {

        } else
        {
            if(activeTile != null)
            {
                activeTile.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if(direction == "W")
            {
                tiles[0].GetComponent<SpriteRenderer>().color = Color.green;
                activeTile = tiles[0];
            } else if (direction == "S")
            {
                tiles[1].GetComponent<SpriteRenderer>().color = Color.green;
                activeTile = tiles[1];
            } else if (direction == "A")
            {
                tiles[2].GetComponent<SpriteRenderer>().color = Color.green;
                activeTile = tiles[2];
            }
            else if (direction == "D")
            {
                tiles[3].GetComponent<SpriteRenderer>().color = Color.green;
                activeTile = tiles[3];
            }
        }  
    }

    public void setupAttack(Vector3 playerPosition)
    {
        for(int i = 0; i < possibleAttackPositions.Length; i++)
        {
            tiles[i] = RedTilePopup.create(playerPosition + possibleAttackPositions[i]);
        }
    }

    public void applyAttack(GameObject player, Vector3 position)
    {

        GameObject[] characters = GameObject.FindGameObjectsWithTag("character");
        for(int i = 0; i <  characters.Length; i++)
        {
            if(characters[i].transform.position.x == position.x)
            {
                characters[i].GetComponent<Character>().simulateDamage(damage);
            }
        }
    }
   
}
