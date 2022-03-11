using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roublard : Character
{
    public Sprite hiddenSprite;
    public bool hidden;
    public int hiddenDuration;

    public Roublard(Vector3 position, bool isBlue) : base(position,  100, 50, isBlue)
    {
        Debug.Log("coucou");
        hidden = false;
        characterType = type.roublard;
        hiddenDuration = 0;
        skill1CastTime = 1;
        skill1CoolDownTime = 5;
        skill2CastTime = 1;
        skill2CoolDownTime = 15;
    }

    public void init(Vector3 position, bool isBlue) {
        base.init(position,  100, 50, isBlue);
        Debug.Log("coucou");
        hidden = false;
        characterType = type.roublard;
        hiddenDuration = 0;
        skill1CastTime = 1;
        skill1CoolDownTime = 5;
        skill2CastTime = 1;
        skill2CoolDownTime = 15;
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

    public override void attack()
    {
        if (hidden)
        {
            hidden = false;
            hiddenDuration = 0;
        }
        base.attack();
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