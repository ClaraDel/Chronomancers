using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paladin : Character
{
    public Sprite blockingSprite;
    public bool blocking { get; set; }
    public bool waited;

    public void init(bool isBlue) {
        base.init(200, 50, isBlue);
        blocking = false;
        characterType = type.paladin;
        waited = false;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 6;
        skill2CastTime = 2;
        maxCoolDownSkill2 = 5;
    }

    public override void reset()
    {
        blocking = false;
        waited = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        base.reset();
    }

    public void action()
    {
        blocking = false;
        waited = false;
    }

    public override void wait()
    {
        if (waited == false)
        {
            waited = true;
            blocking = true;
        } else
        {
            blocking = false;
        }
        base.wait();
    }

    public override void takeDamage(Character attacker, int damage)
    {
        if (!blocking)
        {
            base.takeDamage(attacker, damage);
        }
        else
        {
            if (shielded)
            {
                shielded = false;
                shieldDuration = 0;
            }
            else
            {
                blocking = false;
            }
        }
    }

    public override void moveH()
    {
        action();
        base.moveH();
    }

    public override void moveV()
    {
        action();
        base.moveV();
    }

    public override void addAttack()
    {
        action();
        base.setUpAttack();
    }

    // Imposition des mains
    public override void castSkill1()
    {
        // R�cup�rer case cible et ajouter 50 pvs en utilisant heal sur tous les persos
    }

    // Stealth
    public override void castSkill2()
    {
        // Trouver un moyen de faire en sorte que cooldown ne descende que si le bouclier n'existe plus
        // Donner shield aux persos gr�ce � collision et � m�thode shield
    }

}