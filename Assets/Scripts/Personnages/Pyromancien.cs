using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyromancien : Character
{
    public GameObject fireWallPrefab;
    public List<List<GameObject>> fireWalls;
    private Animator pyroAnim;
    [SerializeField] Transform Burst;
    [SerializeField] Transform Bomb;
    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.pyromancien;
        skill1CastTime = 4;
        maxCoolDownSkill1 = 8;
        skill2CastTime = 2;
        maxCoolDownSkill2 = 10;
        AbilityTimer.instance.getAbility(1).setCountTimer(coolDownSkill1 - 1);
        AbilityTimer.instance.launchUIAbility(1);
        AbilityTimer.instance.getAbility(2).setCountTimer(coolDownSkill2 - 1);
        AbilityTimer.instance.launchUIAbility(2);
        fireWalls = new List<List<GameObject>>();
        pyroAnim = transform.GetComponent<Animator>();
    }

    public override void reset()
    {
        base.reset();
    }

    public override void addAttack()
    {        

        castingTicks = 1;

        CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
        Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
        for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
        {
            positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
        }

        TimeManager.instance.AddAction(() => castAttack(positions, cursor.direction));
        AttackManager.instance.addFutureAttack(this, positions, normalAttackDamage, 1);
        this.StartCoroutine(TimeManager.instance.PlayTick());
        this.StartCoroutine(TimeManager.instance.PlayTick());

        this.cursor.GetComponent<CursorManager>().reset();
        this.cursor.SetActive(false);
        this.StartCoroutine(TimeManager.instance.PlayTick());
        Attack.Play();
    }

    public override void castAttack(Vector3[] positions, CursorManager.directions direction)
    {
        pyroAnim.Play("Hit1Pyromancien");
        for (int i = 0; i < positions.Length; i++)
        {
            Instantiate(Burst, positions[i]- new Vector3(0.5f, 0.5f, 0), transform.rotation);
        }
    }

    // Boule de feu
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            AttackManager.instance.attackTiles(this, positions, 100);
            // Mettre animation ici
            Debug.Log("launchSkill1 : boule de feu");
        }
    }

    public override void setUpSkill2()
    {
        this.zoneSkill2.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUpFirewall(zoneSkill2);
    }

    // Mur de feu
    public override void castSkill2()
    {
        pyroAnim.Play("castSkill2Pyromancien");
        if (coolDownSkill2 == 0)
        {
            castingTicks = skill2CastTime - 1;
            castingSkill2 = true;
            coolDownSkill2 = maxCoolDownSkill2 + skill2CastTime;

            CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
            Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
            for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
            {
                positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
            }

            List<GameObject> newFireWall = new List<GameObject>();

            // Creer murs de feu
            foreach (Vector3 position in positions)
            {
                newFireWall.Add(Instantiate(fireWallPrefab, position - new Vector3(0.5f, 0.5f, 0), transform.rotation, null));
            }

            fireWalls.Add(newFireWall);
            int index = fireWalls.Count;

            TimeManager.instance.AddFutureAction(() => launchSkill2(index-1), skill2CastTime - 1);

            StartCoroutine(TimeManager.instance.PlaySeveralTicks(skill2CastTime + 1));
            Attack.PlayOneShot(Ability2);
        }
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    public void launchSkill2(int index)
    {
        if (alive)
        {
            foreach (GameObject fireWall in fireWalls[index])
            {
                fireWall.GetComponent<FireWall>().setSelf();
                Instantiate(Bomb, fireWall.transform.position , transform.rotation);
            }
        }
    }

    public override void moveH(float sens)
    {
        pyroAnim.Play("RunPyromancien");
        base.moveH(sens);
    }

    public override void moveV(float sens)
    {
        pyroAnim.Play("RunPyromancien");
        base.moveV(sens);
    }

    public override void takeDamage(Character attacker, int damage)
    {
        if (alive)
        {
            pyroAnim.Play("HurtPyromancien");
        }
        base.takeDamage(attacker, damage);
    }
}