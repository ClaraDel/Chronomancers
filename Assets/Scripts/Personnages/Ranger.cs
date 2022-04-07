using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranger : Character
{

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.ranger;
        skill1CastTime = 2;
        coolDownSkill1 = 10;
        skill2CastTime = 0;
        coolDownSkill2 = 7;
    }

    public virtual void castSkill1()
    {
        if (coolDownSkill1 == 0)
        {
            coolDowns();
            castingTicks = skill1CastTime - 1;
            castingSkill1 = true;
            coolDownSkill1 = maxCoolDownSkill1 + skill1CastTime;

            GameObject cursor = gameObject.transform.Find("Cursor").gameObject;

            TimeManager.instance.AddAction(() => launchSkill1(cursor));
            AttackManager.instance.addFutureAttack(this, cursor, zoneSkill1, 75, skill1CastTime + 1);
            StartCoroutine(TimeManager.instance.PlayTick());
        }
    }

    // Tir pr�cis
    public override void launchSkill1(GameObject cursor)
    {
        if (alive)
        {
        }
    }

    // Dash
    public override void launchSkill2(GameObject cursor)
    {
        // Ajouter mouvement vers case ciblee a 3 de port�e
        gameObject.transform.position = zoneSkill2.getTilesEffets()[0].transform.position;
    }

}