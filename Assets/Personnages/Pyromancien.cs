using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyromancien : Character
{
    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.pyromancien;
        skill1CastTime = 4;
        skill1CoolDownTime = 8;
        skill2CastTime = 2;
        skill2CoolDownTime = 10;
    }

    public override void reset()
    {
        base.reset();
    }

    public override void addAttack()
    {
        
        GameObject Cursor = gameObject.transform.Find("Cursor").gameObject;
        
        base.wait();
        
        AttackManager.instance.addFutureAttack(this, Cursor, zoneBasicAttack, normalAttackDamage, TimeManager.currentTick);

        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        Cursor.GetComponent<CursorManager>().gameObject.SetActive(false);
        coolDowns();
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