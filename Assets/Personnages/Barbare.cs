using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barbare : Character
{
    public Sprite enragedSprite;
    public bool enraged;
    public int rageDuration;

    public Barbare(Vector3 position, bool isBlue) : base(position,  150, 50, isBlue)
    {
        enraged = false;
        characterType = type.barbare;
        rageDuration = 0;
        skill1CastTime = 1;
        skill1CoolDownTime = 5;
        skill2CastTime = 0;
        skill2CoolDownTime = 7;
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

    public override void moveH()
    {
        testEnraged();
        base.moveH();
    }

    public override void moveV()
    {
        testEnraged();
        base.moveV();
    }

    public override void attack()
    {
        testEnraged();
        if (enraged)
        {
            atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 } }
       , 2 * normalAttackDamage, this, 1, 1
           );
            atk.setupAttack(position);
            base.coolDowns();
        }
        else
        {
            base.attack();
        }
    }

    // GRO TAPE
    public override void launchSkill1()
    {
        testEnraged();
        if (enraged)
        {
            atk = new Attack(new[] {
        new Vector3 { x = 1, y = 0, z = 0 },
        new Vector3 { x = 1, y = 0, z = 1 },
        new Vector3 { x = 1, y = 0, z = -1 } }
        , 2 * normalAttackDamage, this, 1, 1
            );
            atk.setupAttack(position);
        }
        else
        {
            atk = new Attack(new[] {
        new Vector3 { x = 1, y = 0, z = 0 },
        new Vector3 { x = 1, y = 0, z = 1 },
        new Vector3 { x = 1, y = 0, z = -1 } }
        , normalAttackDamage, this, 1, 1
            );
            atk.setupAttack(position);
        }
        base.launchSkill1();
    }

    public override void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            castingTicks = skill2CastTime;
            coolDownSkill1 = skill2CoolDownTime;
        }
        launchSkill2();
        base.coolDowns();
    }

    // CROOOoom !
    public override void launchSkill2()
    {
        //TODO : Rush + Stop si collision et dégâts
        base.launchSkill2();
    }

}