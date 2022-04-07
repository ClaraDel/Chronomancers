using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barbare : Character
{
    public Sprite enragedSprite;
    public bool enraged;
    public int rageDuration;

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        enraged = false;
        characterType = type.barbare;
        rageDuration = 0;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 5;
        skill2CastTime = 0;
        maxCoolDownSkill2 = 7;
    }

    public override void reset()
    {
        enraged = false;
        rageDuration = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        base.reset();
    }

    public void testEnraged() 
    {
        if (enraged)
        {
            if (rageDuration == 0)
            {
                enraged = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
            }
            else
            {
                rageDuration--;
            }
        }
    }

    public override void takeDamage(Character attacker, int damage) 
    {
        enraged = true;
        rageDuration = 5;
        base.takeDamage(attacker, damage);
    }

    public override void wait()
    {
        testEnraged();
        base.wait();
    }

    public override void moveH(float sens)
    {
        testEnraged();
        base.moveH(sens);
    }

    public override void moveV(float sens)
    {
        testEnraged();
        base.moveV(sens);
    }

    public override void addAttack()
    {
        GameObject Cursor = gameObject.transform.Find("Cursor").gameObject;
        if (enraged)
        {
            AttackManager.instance.addAttack(this, Cursor, zoneBasicAttack, 2*normalAttackDamage);
        }
        else
        {
            AttackManager.instance.addAttack(this, Cursor, zoneBasicAttack, normalAttackDamage);
        }
        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        Cursor.GetComponent<CursorManager>().gameObject.SetActive(false);
        coolDowns();
    }

    public virtual void setUpSkill1()
    {
        this.zoneSkill1.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        gameObject.transform.Find("Cursor").GetComponent<CursorManager>().setUpRotation(zoneSkill1);
    }

    // GRO TAPE
    public override void launchSkill1(GameObject cursor)
    {
        if (alive)
        {
            testEnraged();
            if (enraged)
            {
                AttackManager.instance.attackTiles(this, cursor, zoneSkill1, 100);
            }
            else
            {
                AttackManager.instance.attackTiles(this, cursor, zoneSkill1, 50);
            }
        }
    }

    public virtual void setUpSkill2()
    {
        this.zoneSkill2.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        gameObject.transform.Find("Cursor").GetComponent<CursorManager>().setUpRotation(zoneSkill2);
    }

    /*
    public virtual void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            coolDowns();
            castingTicks = skill2CastTime - 1;
            castingSkill2 = true;
            coolDownSkill2 = maxCoolDownSkill2 + skill2CastTime + 3;

            GameObject cursor = gameObject.transform.Find("Cursor").gameObject;

            TimeManager.instance.AddFutureAction(() => launchSkill2(cursor), skill1CastTime);
            StartCoroutine(TimeManager.instance.PlayTick());
        }
    }
    */

    // CROOOoom !
    public override void launchSkill2(GameObject cursor)
    {
        if (alive)
        {
            // if (coolDownSkill2 == 0)
            // {
            //     castingTicks = skill2CastTime;
            //     coolDownSkill1 = skill2CoolDownTime;
            // }
            // launchSkill2();
            // base.coolDowns();
        }
    }
}