using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barbare : Character
{
    public Sprite enragedSprite;
    public bool enraged;
    public int rageDuration;
    private Animator barbareAnim;

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        enraged = false;
        characterType = type.barbare;
        rageDuration = 0;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 5;
        skill2CastTime = 0;
        maxCoolDownSkill2 = 7;
        barbareAnim = transform.GetComponent<Animator>();
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
        if (sens > 0)
        {
            barbareAnim.Play("runBarbareR");
        }
        else if (sens < 0)
        {
            barbareAnim.Play("runBarbare");
        }
    }

    public override void moveV(float sens)
    {
        testEnraged();
        barbareAnim.Play("RunBarbare");
        base.moveV(sens);
    }

    public override void addAttack()
    {
        barbareAnim.Play("hit1Barbare");
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

    // GRO TAPE
    public override void launchSkill1(GameObject cursor)
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

    // CROOOoom !
    public override void launchSkill2(GameObject cursor)
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