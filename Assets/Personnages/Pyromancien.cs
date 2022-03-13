using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyromancien : Character
{
    public bool attacking;



    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.pyromancien;
        skill1CastTime = 4;
        maxCoolDownSkill1 = 8;
        skill2CastTime = 2;
        maxCoolDownSkill2 = 10;
        attacking = false;
    }

    public override void cast()
    {
        if (attacking)
        {
            attacking = false;
            launchAttack();
        }
        base.cast();
    }

    public override void setUpAttack()
    {
        castingTicks = 1;
        base.setUpAttack();
    }

    public void launchAttack()
    {
        castingTicks = 1;
        base.coolDowns();
    }

    // Explosion
    public override void launchSkill1()
    {
        
        base.launchSkill1();
    }

    // Stealth
    public override void launchSkill2()
    {
        // Creer objet mur de feu et le faire spawner
        base.launchSkill2();
    }

}