using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance { get; set; }

    void Start(){
        instance = this;
    }

    public void addAttack(Character attacker, Vector3[] positions, int damage){
        TimeManager.instance.AddAction(() => attackTiles(attacker, positions, damage));
    }

    public void addFutureAttack(Character attacker, Vector3[] positions, int damage, int tick)
    {
        TimeManager.instance.AddFutureAction(() => attackTiles(attacker, positions, damage), tick);
    }

    public void attackTiles(Character attacker, Vector3[] positions, int damage)
    {
        if (attacker.isAlive())
        {
            foreach (Vector3 cible in positions)
            {
                attackTile(attacker, cible, damage);
            }
        }
    }
    public void endAtk()
    {

    }

    public void attackTile(Character attacker, Vector3 cible, int damage)
    {
        Debug.Log("Attack Tile");
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(cible, Vector3.forward);
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
