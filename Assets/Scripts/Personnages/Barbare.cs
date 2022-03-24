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

    public override void setUpAttack()
    {
        testEnraged();
        if (enraged)
        {
            // atk = new AttackManager(new[] {
            // new Vector3 { x = 0, y = 0, z = 0 } }, normalAttackDamage, this, 1, 1);
            // atk.setupAttack(position);
            // base.coolDowns();
        }
        else
        {
            // base.setUpAttack();
        }
    }

    // GRO TAPE
    public override void launchSkill1()
    {
        // testEnraged();
        // if (enraged)
        // {
        //     atk = new AttackManager(new[] {
        // new Vector3 { x = 1, y = 0, z = 0 },
        // new Vector3 { x = 1, y = 0, z = 1 },
        // new Vector3 { x = 1, y = 0, z = -1 } }
        // , 2 * normalAttackDamage, this, 1, 1
        //     );
        //     atk.setupAttack(position);
        // }
        // else
        // {
        //     atk = new AttackManager(new[] {
        // new Vector3 { x = 1, y = 0, z = 0 },
        // new Vector3 { x = 1, y = 0, z = 1 },
        // new Vector3 { x = 1, y = 0, z = -1 } }
        // , normalAttackDamage, this, 1, 1
        //     );
        //     atk.setupAttack(position);
        // }
        // base.launchSkill1();
    }

    public override void castSkill2()
    {
        // if (coolDownSkill2 == 0)
        // {
        //     castingTicks = skill2CastTime;
        //     coolDownSkill1 = skill2CoolDownTime;
        // }
        // launchSkill2();
        // base.coolDowns();
    }

    // CROOOoom !
    public override void launchSkill2()
    {
        //TODO : Rush + Stop si collision et d�g�ts
        // base.launchSkill2();
    }

}