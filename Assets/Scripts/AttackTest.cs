using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : Attack
{
    public int damage
    {
        get
        {
            return 50;
        }
    }

    public void applyAttack(GameObject player, Vector3 position)
    {
        int x1 = (int)position.x + 1;
        int x2 = x1 - 1;
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
