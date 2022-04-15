using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barbare : Character
{
    public Sprite enragedSprite;
    public bool enraged;
    public int rageDuration;

    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    private Animator barbareAnim;

    public void init(bool isBlue)
    {
        base.init(100, 50, isBlue);
        enraged = false;
        characterType = type.barbare;
        rageDuration = 0;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 5;
        skill2CastTime = 0;
        maxCoolDownSkill2 = 7;
        AbilityTimer.instance.getAbility(1).setCountTimer(coolDownSkill1 - 1);
        AbilityTimer.instance.launchUIAbility(1);
        AbilityTimer.instance.getAbility(2).setCountTimer(coolDownSkill2 - 1);
        AbilityTimer.instance.launchUIAbility(2);
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
        barbareAnim.Play("runBarbare");
        base.moveV(sens);
    }

    public override void addAttack()
    {
        CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
        Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
        for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
        {
            positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
        }
        if (enraged)
        {
            TimeManager.instance.AddAction(() => castAttack(positions, cursor.direction, 100));
        }
        else
        {
            TimeManager.instance.AddAction(() => castAttack(positions, cursor.direction, 50));
        }
        StartCoroutine(TimeManager.instance.PlayTick());
        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        cursor.gameObject.SetActive(false);
    }

    public void castAttack(Vector3[] positions, CursorManager.directions direction, int damage)
    {
        AttackManager.instance.attackTiles(this, positions, damage);
        CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
        if (cursor.direction == CursorManager.directions.right)
        {
            print(cursor.direction);
            barbareAnim.Play("hit1BarbareR");
        }
        else
        {
            barbareAnim.Play("hit1Barbare");
        }
    }

    public override void setUpSkill1()
    {
        this.zoneSkill1.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUpRotation(zoneSkill1);
    }

    // GRO TAPE
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            testEnraged();
            if (enraged)
            {
                AttackManager.instance.attackTiles(this, positions, 100);
            }
            else
            {
                AttackManager.instance.attackTiles(this, positions, 50);
            }
        }
        this.zoneSkill1.getZoneCiblable().SetActive(false);
        cursor.SetActive(false);
    }

    public override void setUpSkill2()
    {
        this.zoneSkill2.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUpRotation(zoneSkill2);
    }

    public override void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            coolDowns();
            castingTicks = skill2CastTime - 1;
            castingSkill2 = true;
            coolDownSkill2 = maxCoolDownSkill2 + skill2CastTime + 3;

            CursorManager cursorManager = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();

            if (cursorManager.direction == CursorManager.directions.up)
            {
                TimeManager.instance.AddFutureAction(() => launchSkill2(up), skill1CastTime);
                for (int i = 0; i < 3; i++)
                {
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(0, 1), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(up), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(0, 1), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(up), skill1CastTime + i);
                }
            }
            else if (cursorManager.direction == CursorManager.directions.left)
            {
                TimeManager.instance.AddFutureAction(() => launchSkill2(left), skill1CastTime);
                for (int i = 0; i < 3; i++)
                {
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(-1, 0), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(left), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(-1, 0), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(left), skill1CastTime + i);
                }
            }
            else if (cursorManager.direction == CursorManager.directions.down)
            {
                TimeManager.instance.AddFutureAction(() => launchSkill2(down), skill1CastTime);
                for (int i = 0; i < 3; i++)
                {
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(0, -1), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(down), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(0, -1), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(down), skill1CastTime + i);
                }
            }
            else if (cursorManager.direction == CursorManager.directions.right)
            {
                TimeManager.instance.AddFutureAction(() => launchSkill2(right), skill1CastTime);
                for (int i = 0; i < 3; i++)
                {
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(1, 0), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(right), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => moveManager.AddMove(1, 0), skill1CastTime + i);
                    TimeManager.instance.AddFutureAction(() => bashAttack(right), skill1CastTime + i);
                }
            }

            StartCoroutine(TimeManager.instance.PlayTick());
            this.zoneSkill2.getZoneCiblable().SetActive(false);
            cursor.SetActive(false);
        }
    }

    public void bashAttack(Vector3 direction)
    {
        if (enraged)
        {
            AttackManager.instance.attackTileRelative(gameObject, direction, 2 * normalAttackDamage);
        }
        else
        {
            AttackManager.instance.attackTileRelative(gameObject, direction, normalAttackDamage);
        }
    }

    // CROOOoom !
    public void launchSkill2(Vector3 direction)
    {
        if (alive)
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
}