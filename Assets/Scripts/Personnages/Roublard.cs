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

    public void init(bool isBlue) {
        base.init(100, 50, isBlue);
        hidden = false;
        characterType = type.roublard;
        hiddenDuration = 0;
        skill1CastTime = 1;
        skill1CoolDownTime = 5;
        skill2CastTime = 1;
        skill2CoolDownTime = 15;
        roublardAnim = transform.GetComponent<Animator>();
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

    public override void moveH(float sens)
    {
        testHidden();
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
        testHidden();
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

    public override void setUpAttack()
    {
        if (hidden)
        {
            hidden = false;
            hiddenDuration = 0;
        }
        base.setUpAttack();
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