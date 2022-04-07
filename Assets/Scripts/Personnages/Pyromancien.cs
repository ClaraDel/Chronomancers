using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyromancien : Character
{
    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.pyromancien;
        skill1CastTime = 4;
        maxCoolDownSkill1 = 8;
        skill2CastTime = 2;
        maxCoolDownSkill2 = 10;
    }

    public override void reset()
    {
        base.reset();
    }

    public override void addAttack()
    {
        
        GameObject Cursor = gameObject.transform.Find("Cursor").gameObject;
        
        base.wait();
        
        AttackManager.instance.addFutureAttack(this, Cursor, zoneBasicAttack, normalAttackDamage,1);

        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        Cursor.GetComponent<CursorManager>().gameObject.SetActive(false);
        TimeManager.instance.PlayTick();
    }

    /*public void launchAttack()
    {
        castingTicks = 1;
        base.coolDowns();
    }*/

    // Boule de feu
    public override void launchSkill1(GameObject cursor)
    {
        if (alive)
        {
            AttackManager.instance.attackTiles(this, cursor, zoneSkill1, 100);
            // Mettre animation ici
        }
    }

    // Mur de feu
    public override void launchSkill2(GameObject cursor)
    {
        if (alive)
        {
            // Creer murs de feu
        }
    }
}