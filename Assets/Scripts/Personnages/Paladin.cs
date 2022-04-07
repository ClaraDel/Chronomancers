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
        coolDownSkill1 = 6;
        skill2CastTime = 2;
        coolDownSkill2 = 10;
        paladinAnim = transform.GetComponent <Animator> ();
    }

    public override void reset()
    {
        blocking = false;
        waited = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        base.reset();
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
            base.takeDamage(attacker, damage);
        }
        else
        {
            if (shielded)
            {
                shielded = false;
                shieldDuration = 0;
            }
            else
            {
                blocking = false;
            }
        }
    }

    public override void die()
    {
        paladinAnim.Play("deathPaladin");
        base.die();
    }

    /*public override void addAttack()
    {
        //paladinAnim.Play("hitPaladin");
    }*/

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

    public override void addAttack()
    {
        action();
        base.addAttack();
    }

    // Imposition des mains
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            foreach (var cible in positions)
            {

                RaycastHit2D[] hits;
                hits = Physics2D.RaycastAll(cible, Vector3.forward);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider != null)
                    {
                        Character target = hits[i].collider.gameObject.GetComponent<Character>();
                        target.heal(50);
                    }
                }
            }
            paladinAnim.Play("hillPaladin");
        }
    }

    // Shield
    public override void launchSkill2(Vector3[] positions)
    {
        if (alive)
        {
            foreach (var cible in positions)
            {
                RaycastHit2D[] hits;
                hits = Physics2D.RaycastAll(cible, Vector3.forward);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider != null)
                    {
                        Character target = hits[i].collider.gameObject.GetComponent<Character>();
                        target.shield(6);
                    }
                }
            }
            paladinAnim.Play("hillPaladin");
        }
    }

}