using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranger : Character
{
    private Animator rangerAnim;
    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.ranger;
        skill1CastTime = 2;
        coolDownSkill1 = 10;
        skill2CastTime = 0;
        coolDownSkill2 = 7;
        AbilityTimer.instance.getAbility(1).setCountTimer(coolDownSkill1 - 1);
        AbilityTimer.instance.launchUIAbility(1);
        AbilityTimer.instance.getAbility(2).setCountTimer(coolDownSkill2 - 1);
        AbilityTimer.instance.launchUIAbility(2);

        rangerAnim = transform.GetComponent<Animator>();
    }

    public override void castAttack(Vector3[] positions, CursorManager.directions direction)
    {
        // anim goes there
        base.castAttack(positions, direction);
    }

    public override void castSkill1()
    {
        print("castSkill1 ranger" + coolDownSkill1);
        Debug.Log("HitRangerR");
        if (coolDownSkill1 == 0)
        {
            coolDowns();
            castingTicks = skill1CastTime - 1;
            castingSkill1 = true;
            coolDownSkill1 = maxCoolDownSkill1 + skill1CastTime;

            CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
            Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
            print("positionX = " + cursor.getPositionX() + "positionY = " + cursor.getPositionY());
            if (cursor.direction == CursorManager.directions.right) {
                rangerAnim.Play("HitRangerR");
                Debug.Log("HitRangerR");
            }
            else
            {
                rangerAnim.Play("HitRanger");
                Debug.Log("HitRanger");
            }

            for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
            {
                positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
            }

            TimeManager.instance.AddAction(() => launchSkill1(positions));
            AttackManager.instance.addFutureAttack(this, positions, 75, skill1CastTime + 1);
            StartCoroutine(TimeManager.instance.PlayTick());
        }
    }

    // Tir pr�cis
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
        }
    }

    // Dash
    public override void launchSkill2(Vector3[] positions)
    {
        // Ajouter mouvement vers case ciblee a 3 de port�e
        // TODO Check if target out of bounds
        gameObject.GetComponent<PlayerController>().PlayerTarget.position = positions[0];
    }

    public override void moveH(float sens)
    {
        if (sens > 0)
        {
            rangerAnim.Play("RunRangerR");
        }
        else
        {
            rangerAnim.Play("RunRanger");
        }
        base.moveH(sens);
    }

    public override void moveV(float sens)
    {
        base.moveV(sens);
        rangerAnim.Play("RunRanger");
    }

    public override void takeDamage(Character attacker, int damage)
    {
        if (alive)
        {
            rangerAnim.Play("HurtRanger");
        }
        base.takeDamage(attacker, damage);
    }

}