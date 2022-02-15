using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : Attack
{

    public List <RedTilePopup> [] tiles = new List <RedTilePopup> [4];
    public enum directions
    {
        right,
        up,
        left,
        down
    }
    public Vector3 playerPosition;
    public List<RedTilePopup> activeTiles;
    int currentDirection;
    public Vector3[] positions;
    int dmg;
    public CursorManager cursor;

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
        int nb = activeTiles.Count;
        for (int i = 0; i < nb; i++)
        {
            RedTilePopup tmp = activeTiles[activeTiles.Count - 1];
            activeTiles.Remove(tmp);
            cursor.destroy();
            tmp.destroy(); ;
        }
    }

    public void selectAttack(string direction)
    {
        int offset = 0;
        
        if(tiles.Length == 0)
        {

        } else
        {
            if(activeTiles != null)
            {
                int nb = activeTiles.Count;
                for(int i = 0; i < nb; i++){
                    RedTilePopup tmp = activeTiles[activeTiles.Count - 1];
                    activeTiles.Remove(tmp);
                    cursor.destroy();
                    tmp.destroy();
                }
            }

            if(direction == "W")
            {
                offset =  currentDirection - (int) directions.up;
                currentDirection = (int)directions.up;

            } else if (direction == "A")
            {
                offset = currentDirection  - (int) directions.left;
                currentDirection = (int)directions.left;
            }
            else if (direction == "S")
            {
                offset = currentDirection  - (int) directions.down;
                currentDirection = (int)directions.down;
            }
            else if (direction == "D")
            {
                offset = currentDirection - (int) directions.right;
                currentDirection = (int)directions.right;
            }

        projectPosition(possibleAttackPositions, (Mathf.PI/2)*(-offset));
        createRedTiles(possibleAttackPositions, playerPosition);
        cursor = CursorManager.create(possibleAttackPositions[0] + playerPosition, activeTiles);

        } 
    }


    public void projectPosition(Vector3 [] positions, float theta)
    {

        for (int i = 0; i < positions.Length; i++)
        {
            float tmp = positions[i].x*Mathf.Cos(theta) - positions[i].y * Mathf.Sin(theta);
            positions[i].y = Mathf.RoundToInt(positions[i].x * Mathf.Sin(theta) + positions[i].y * Mathf.Cos(theta)); 
            positions[i].x = Mathf.RoundToInt(tmp);
        }
        
    }


    public void createRedTiles(Vector3[] positions, Vector3 playerPosition)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            RedTilePopup tmp = RedTilePopup.create(playerPosition + possibleAttackPositions[i]);
            activeTiles.Add(tmp);
        }
    }

    public void setupAttack(Vector3 playerPosition)
    {

        activeTiles = new List<RedTilePopup>();
        currentDirection = (int) directions.right;
        this.playerPosition = playerPosition;
        createRedTiles(possibleAttackPositions, playerPosition);
        cursor = CursorManager.create(possibleAttackPositions[0] + playerPosition, activeTiles);

    }
    public void applyAttack(Vector3 [] positions)
    {
        Vector3 position1 = positions[0];
        Vector3 position2 = positions[1];
        Vector3 dir2target = -position1 + position2;
        float lenRay = Vector3.Distance(position1, position2);
        RaycastHit2D hit = Physics2D.Raycast(position1,
            dir2target, lenRay);

        if (hit.collider != null)
        {
            Character target = hit.collider.gameObject.GetComponent<Character>();
            target.simulateDamage(damage);
        }
    }
    public void applyAttack()
    {
        Vector3 cursorPos = cursor.transform.position;

        TimeManager.instance.AddAction(() => applyAttack(new[] {playerPosition,cursorPos}));
        TimeManager.instance.PlayTick();
    }
   
}
