using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance { get; set; }

    void Start(){
        instance = this;
    }

    public void addAttack(Character attacker, GameObject cursor, Zone zone, int damage){
        TimeManager.instance.AddAction(() => attackTiles(attacker, cursor, zone, damage));
        StartCoroutine(TimeManager.instance.PlayTick());
    }

    public void addFutureAttack(Character attacker, GameObject cursor, Zone zone, int damage, int tick)
    {
        TimeManager.instance.AddFutureAction(() => attackTiles(attacker, cursor, zone, damage), tick);
        StartCoroutine(TimeManager.instance.PlayTick());
    }

    public void attackTiles(Character attacker, GameObject cursor, Zone zone, int damage)
    {
        if (attacker.isAlive())
        {
            foreach (var tiles in zone.getTilesEffets())
            {
                Vector3 cible = tiles.transform.position;
                attackTile(attacker, cible, damage);
            }
        }
    }

    public void attackTile(Character attacker, Vector3 cible, int damage)
    {
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
}
