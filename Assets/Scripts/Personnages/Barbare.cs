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
        barbareAnim = transform.GetComponent<Animator>();
    }

    public override void reset()
    {
        enraged = false;
        rageDuration = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        base.reset();
    }

    public override void coolDowns()
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

    public override void moveH(float sens)
    {
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
        wait();
        cursor.GetComponent<CursorManager>().reset();
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
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUpRotation(zoneSkill1);
    }

    // GRO TAPE
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            if (enraged)
            {
                AttackManager.instance.attackTiles(this, positions, 100);
            }
            else
            {
                AttackManager.instance.attackTiles(this, positions, 50);
            }
        }
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
            castingTicks = skill2CastTime + 3;
            castingSkill2 = true;
            coolDownSkill2 = maxCoolDownSkill2 + skill2CastTime + 3;

            CursorManager cursorManager = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
            switch (cursorManager.direction)
            {
                case CursorManager.directions.up:
                    StartCoroutine(charge(0, 1));
                    break;
                case CursorManager.directions.right:
                    StartCoroutine(charge(1, 0));
                    break;
                case CursorManager.directions.down:
                    StartCoroutine(charge(0, -1));
                    break;
                case CursorManager.directions.left:
                    StartCoroutine(charge(-1, 0));
                    break;
            }
            cursor.GetComponent<CursorManager>().reset();
            cursor.SetActive(false);
        }
    }

    public IEnumerator charge(int x, int y){
        for (int i = 0; i < 3; i++)
        {
            Vector3 start = new Vector3(transform.position.x + 0.5f + x/1.5f, transform.position.y + 0.5f + y/1.5f, 0f);
            Vector3 dir = new Vector3(x, y, 0f);
            RaycastHit hit;
            RaycastHit2D hit2D1f = Physics2D.Raycast(start, dir, 0f);
            RaycastHit2D hit2D2f = Physics2D.Raycast(start, dir, 1f);
            if ((Physics.Raycast(start, dir, out hit, 0.1f) && hit.transform.tag == "Wall") || (hit2D1f.transform != null && hit2D1f.transform.tag == "character")){
                if(hit2D1f.transform != null && hit2D1f.transform.tag == "character"){
                    TimeManager.instance.AddAction(() => bashAttack(new Vector3(x,y,0)));
                }
                yield return StartCoroutine(TimeManager.instance.PlayTick()); //Tick de stun
                break; // On sort de la charge
            } else if ((Physics.Raycast(start, dir, out hit, 1.1f) && hit.transform.tag == "Wall") || (hit2D2f.transform != null && hit2D2f.transform.tag == "character")) {
                if(hit2D2f.transform != null && hit2D2f.transform.tag == "character") {
                    TimeManager.instance.AddAction(() => bashAttack(new Vector3(x,y,0)));
                    TimeManager.instance.AddAction(() => moveManager.AddMove(x, y));
                } else if (hit.transform.tag == "Wall"){
                    TimeManager.instance.AddAction(() => moveManager.AddMove(x, y));
                    yield return StartCoroutine(TimeManager.instance.PlayTick()); //Tick pour le déplacement vers le mur
                }
                yield return StartCoroutine(TimeManager.instance.PlayTick()); //Tick pour le déplacement et l'attack ou le stun
                break; // On sort de la charge
            } else {
                TimeManager.instance.AddAction(() => moveManager.AddMove(x, y));
                TimeManager.instance.AddAction(() => moveManager.AddMove(x, y));
                yield return StartCoroutine(TimeManager.instance.PlayTick()); //Tick pour le double déplacement
            }
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