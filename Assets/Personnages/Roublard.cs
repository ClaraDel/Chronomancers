using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roublard : Character
{
    public Sprite hiddenSprite;
    public bool hidden;
    public int hiddenDuration;

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        hidden = false;
        characterType = type.roublard;
        hiddenDuration = 0;
        skill1CastTime = 1;
        maxCoolDownSkill1 = 5;
        skill2CastTime = 1;
        maxCoolDownSkill2 = 15;
    }

    public override void reset()
    {
        hidden = false;
        hiddenDuration = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        base.reset();
    }

    public void testHidden() 
    {
        if (hidden)
        {
            if (hiddenDuration == 0)
            {
                hidden = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
            }
            else
            {
                hiddenDuration--;
            }
        }
    }

    public override void wait()
    {
        testHidden();
        base.wait();
    }

    public override void moveH()
    {
        testHidden();
        if (hidden && !moveAction)
        {
            moveManager.AddDash(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
            moveAction = true;
        }
        else
        {
            base.moveH();
            moveAction = false;
        }
    }

    public override void moveV()
    {
        testHidden();
        if (hidden && !moveAction)
        {
            moveManager.AddDash(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
            moveAction = true;
        } 
        else
        {
            base.moveV();
            moveAction = false;
        }
    }

    public override void addAttack()
    {
        if (hidden)
        {
            hidden = false;
            hiddenDuration = 0;
        }
        base.addAttack();
    }

    // Trap
    public override void castSkill1()
    {
        if (hidden)
        {
            hidden = false;
            hiddenDuration = 0;
        }
        base.castSkill1();
    }

    public override void launchSkill1()
    {
        // Creer objet pi�ge et le faire spawner
        base.launchSkill1();
    }

    public override void launchSkill2()
    {
        hidden = true;
        hiddenDuration = 5;
        gameObject.GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        base.launchSkill2();
    }

}