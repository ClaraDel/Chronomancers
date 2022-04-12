using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyromancien : Character
{
    public GameObject fireWallPrefab;
    public List<List<GameObject>> fireWalls;
    private Animator pyroAnim;
    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        characterType = type.pyromancien;
        skill1CastTime = 4;
        maxCoolDownSkill1 = 8;
        skill2CastTime = 2;
        maxCoolDownSkill2 = 10;
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
        AttackManager.instance.addFutureAttack(this, positions, normalAttackDamage, 1);
        
        TimeManager.instance.AddAction(() => castAttack());

        castingTicks = 1;

        TimeManager.instance.AddAction(() => castAttack());

        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        cursor.GetComponent<CursorManager>().gameObject.SetActive(false);
        TimeManager.instance.PlayTick();
    }

    public override void castAttack()
    {
        /*if (cursor.direction == CursorManager.directions.right)
        {
            pyroAnim.Play("Hit1Pyromancien");
        /*}
        else
        {
            barbareAnim.Play("hit1Pyromancien");
        }*/
    }

    // Boule de feu
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            AttackManager.instance.attackTiles(this, positions, 100);
            // Mettre animation ici
        }
    }

    public override void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            coolDowns();
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
                newFireWall.Add(Instantiate(fireWallPrefab, position, new Quaternion(), null));
            }

            fireWalls.Add(newFireWall);
            int index = fireWalls.Count;

            TimeManager.instance.AddFutureAction(() => launchSkill2(index-1), skill1CastTime - 1);
            StartCoroutine(TimeManager.instance.PlayTick());
        }
    }

    // Mur de feu
    public void launchSkill2(int index)
    {
        if (alive)
        {
            foreach (GameObject fireWall in fireWalls[index])
            {
                fireWall.GetComponent<FireWall>().setSelf();
            }
        }
    }
}