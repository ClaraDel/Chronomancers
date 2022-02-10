using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : Attack
{

    public List <RedTilePopup> [] tiles = new List <RedTilePopup> [4];
    public List<RedTilePopup> activeTile;
    public string directionAtk = "";
    public Vector3[] positions;
    int dmg;

    public AttackTest(Vector3[] positions, int damage) {
        this.positions = positions;
        this.dmg = damage;
    }
    public int damage
    {
        get
        {
            return dmg;
        }
    }




    public Vector3[] possibleAttackPositions { 
        get {
            return positions;  
        }
    }

    public void endAtk()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            for(int j = 0; j < tiles[i].Count; j++)
            {
                GameObject.Destroy(tiles[i][j].gameObject);
            }
        }
    }

    public void selectAttack(string direction)
    {
        if(tiles.Length == 0)
        {

        } else
        {
            if(activeTile != null)
            {
                changeColorListTiles(activeTile, Color.red);
            }
            if(direction == "W")
            {
                changeColorListTiles(tiles[0], Color.green);
                activeTile = tiles[0];
                directionAtk = "W";
            } else if (direction == "A")
            {
                changeColorListTiles(tiles[1], Color.green);
                activeTile = tiles[1];
                directionAtk = "A";
            }
            else if (direction == "S")
            {
                changeColorListTiles(tiles[2], Color.green);
                activeTile = tiles[2];
                directionAtk = "S";

            }
            else if (direction == "D")
            {
                changeColorListTiles(tiles[3], Color.green);
                activeTile = tiles[3];
                directionAtk = "D";

            }
        }  
    }

    public void changeColorListTiles(List <RedTilePopup> redTilePopups, Color color)
    {
        for (int i = 0; i < redTilePopups.Count; i++)
        {
            redTilePopups[i].GetComponent<SpriteRenderer>().color = color;
        }
    }

    public Vector3[] getUpdatedPos(Vector3 [] positions, int i)
    {
       
        if (i == 1)
        {
            projectList(positions);
        } else if(i == 2)
        {
            projectList(positions);
        } else if (i == 3)
        {
            projectList(positions);
        }
        return positions;
    }

    public void projectList(Vector3 [] positions)
    {

        for (int j = 0; j < positions.Length; j++)
        {
            float tmp = positions[j].x;
            positions[j].x = -positions[j].y + 1;
            positions[j].y = tmp;
        }
    }

    public void setupAttack(Vector3 playerPosition)
    {
        for(int i = 0; i < 4; i++)
        {
            tiles[i] = new List<RedTilePopup>();
            Vector3 [] newPositions = getUpdatedPos(possibleAttackPositions, i);
            
            for (int j = 0; j < newPositions.Length; j++)
            {
                RedTilePopup tmp = RedTilePopup.create(playerPosition + newPositions[j]);
                tiles[i].Add(tmp);
            }
        }
    }

    public void applyAttack()
    {

        GameObject[] characters = GameObject.FindGameObjectsWithTag("character");
        if(activeTile == null)
        {

        } else
        {
            for (int i = 0; i < characters.Length; i++)
            {
                for (int j = 0; j < activeTile.Count; j++)
                {
                   
                    if (characters[i].transform.position.y == 
                        activeTile[j].gameObject.transform.position.y - 0.5 &&
                        characters[i].transform.position.x ==
                        activeTile[j].gameObject.transform.position.x - 0.5)
                    {
                        characters[i].GetComponent<Character>().simulateDamage(damage);
                    }
                }

            }
        }
        
    }
   
}
