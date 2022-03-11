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
        skill1CoolDownTime = 10;
        skill2CastTime = 0;
        skill2CoolDownTime = 7;
    }

    public override void setUpAttack()
    {

        base.coolDowns();
    }

    // Tir pr�cis
    public override void launchSkill1()
    {
        // Creer objet fl�che et le faire spawner, puis lui faire infliger des d�gats apr�s 1 tour
        base.launchSkill1();
    }

    // Dash
    public override void launchSkill2()
    {
        // Ajouter mouvement vers case cibl�e � 3 de port�e
        base.launchSkill2();
    }

}