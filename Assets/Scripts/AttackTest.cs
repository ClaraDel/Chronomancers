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
        int x1 = (int)position.x + 20;
        int x2 = x1 - 20;
        GameObject[] characters = GameObject.FindGameObjectsWithTag("character");
        for(int i = 0; i <  characters.Length; i++)
        {
            if(characters[i].transform.position.x <= x1 && characters[i].transform.position.x >= x2)
            {
                characters[i].GetComponent<Character>().simulateDamage(damage);
            }
        }
    }
   
}
