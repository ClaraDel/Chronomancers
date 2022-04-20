using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roublard : Character
{
    public Sprite hiddenSprite;
    public bool hidden;
    public int hiddenDuration;
    private Animator roublardAnim;
    public GameObject trap;

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        hidden = false;
        characterType = type.roublard;
        hiddenDuration = 0;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 5;
        skill2CastTime = 1;
        maxCoolDownSkill2 = 15;
        roublardAnim = transform.GetComponent<Animator>();
    }

    public override void reset()
    {
        hidden = false;
        hiddenDuration = 0;
        transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
        healthBar.SetActive(true);
        //trap.GetComponent<SpriteRenderer>().enabled = false;
        base.reset();
    }

    public override void coolDowns() 
    {
        if (hidden && alive)
        {
            if (hiddenDuration == 0)
            {
                hidden = false;
                transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                healthBar.SetActive(true);
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.1f);
                hiddenDuration--;
            }
        }
        else if(!hidden && alive)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            healthBar.SetActive(true);
        }
        base.coolDowns();
    }

    public override void takeDamage(Character attacker, int damage)
    {
        if (!shielded && alive)
        {
            roublardAnim.Play("hurtRoublardR");
        }
        base.takeDamage(attacker, damage);
    }

    public override void die()
    {
        base.die();
    }

    public override void moveH(float sens)
    {
        if (hidden && !moveAction)
        {
            moveManager.AddDash(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
            moveAction = true;
        }
        else
        {
            if (sens > 0)
            {
                roublardAnim.Play("runRoublardR");
            }
            else if (sens < 0)
            {
                roublardAnim.Play("runRoublard");
            }
            base.moveH(sens);
            moveAction = false;
        }
    }

    public override void moveV(float sens)
    {
        if (hidden && !moveAction)
        {
            moveManager.AddDash(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
            moveAction = true;
        } 
        else
        {
            roublardAnim.Play("runRoublard");
            base.moveV(sens);
            moveAction = false;
        }
    }

    public override void addAttack()
    {
        base.addAttack();
        healthBar.SetActive(true);
        if (hidden && alive)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            hidden = false;
            hiddenDuration = 0;
        }
    }

    public override void castAttack(Vector3[] positions, CursorManager.directions direction)
    {
        roublardAnim.Play("hit1Roublard");
        base.castAttack(positions, direction);
    }

    public override void castSkill1()
    {
        base.castSkill1();
        //trap.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Trap
    public override void launchSkill1(Vector3[] positions)
    {
        if (alive)
        {
            //trap.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("add Trap");
            if (hidden)
            {
                hidden = false;
                hiddenDuration = 0;
            }
            transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            healthBar.SetActive(true);
            Instantiate(trap, positions[0], new Quaternion(), null);
        }
    }

    public override void castSkill2()
    {
        base.castSkill2();
    }

    // Hidden
    public override void launchSkill2(Vector3[] positions)
    {
        if (alive)
        {
            hidden = true;
            hiddenDuration = 5;
            transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.1f);
            healthBar.SetActive(false);
        }
    }

}