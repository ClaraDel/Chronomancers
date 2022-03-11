using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance { get; set; }

    void Start(){
        instance = this;
    }

    public void endAtk()
    {

    }



    public void attackTiles(Character character, Zone zone, int damage)
    {
        if (character.isAlive())
        {
            foreach (var tiles in zone.getTilesEffets())
            {
                attackTile(character, tiles.transform.position, damage);
            }
        }
    }

    public void attackTile(Character attacker, Vector3 cible, int damage)
    {
        RaycastHit2D[] hits;
        Debug.Log(cible);
        hits = Physics2D.RaycastAll(cible+new Vector3(0.5f,0.5f,0.5f), Vector3.forward);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                Character target = hits[i].collider.gameObject.GetComponent<Character>();
                target.takeDamage(attacker, damage);
            }
        }
    }

    // public bool applyAttack()
    // {
    //     if (!afficheur.cursor.isValidPosition())
    //     {
    //         return false;
    //     }

    //     Vector3 cursorPos = afficheur.getCursorPosition();
    //     List<Vector3> zoneEffets = zone.getZoneEffets();

    //     TimeManager.instance.AddAction(() => attackTiles(zoneEffets, cursorPos));
    //     character.StartCoroutine(TimeManager.instance.PlayTick());

    //     return true;
    // }

}
