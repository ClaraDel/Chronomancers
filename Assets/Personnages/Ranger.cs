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

    // Tir pr�cis
    public override void launchSkill1()
    {
        // Creer objet fleche et le faire spawner, puis lui faire infliger des degats apres 1 tour
        base.launchSkill1();
    }

    // Dash
    public override void launchSkill2()
    {
        // Ajouter mouvement vers case ciblee a 3 de port�e
        base.launchSkill2();
    }

}