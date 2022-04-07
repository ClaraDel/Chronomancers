using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paladin : Character
{
    public Sprite blockingSprite;
    public bool blocking { get; set; }
    public bool waited;
    private Animator paladinAnim ;

    public void init(bool isBlue) {
        base.init(200, 50, isBlue);
        blocking = false;
        characterType = type.paladin;
        waited = false;
        skill1CastTime = 1;
        skill1CoolDownTime = 6;
        skill2CastTime = 2;
        skill2CoolDownTime = 5;
        paladinAnim = transform.GetComponent <Animator> ();
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
            paladinAnim.Play("hurtPaladinR");
            base.takeDamage(attacker, damage);
        }
    }

    public override void die()
    {
        //paladinAnim.Play("deathPaladin");
        base.die();
    }

    public override void castAttack(GameObject cursor)
    {
        paladinAnim.Play("hitPaladin");
        base.castAttack(cursor);
    }

    public override void moveH(float sens)
    {
        if (sens > 0)
        {
            paladinAnim.Play("runPaladinR");
        }
        else if (sens < 0)
        {
            paladinAnim.Play("runPaladin");
        }
        action();
        base.moveH(sens);
    }

    public override void moveV(float sens)
    {
        paladinAnim.Play("runPaladin");
        action();
        base.moveV(sens);
    }

    public override void setUpAttack()
    {
        action();
        base.setUpAttack();
    }

    // Imposition des mains
    public override void launchSkill1()
    {
        // R�cup�rer case cible et ajouter 50 pvs en utilisant heal sur tous les persos
        base.launchSkill1();
        paladinAnim.Play("hillPaladin");
    }

    // Stealth
    public override void launchSkill2()
    {
        // Trouver un moyen de faire en sorte que cooldown ne descende que si le bouclier n'existe plus
        // Donner shield aux persos gr�ce � collision et � m�thode shield
        base.launchSkill2();
        paladinAnim.Play("hillPaladin");
    }

}