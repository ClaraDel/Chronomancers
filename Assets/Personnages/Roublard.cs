using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roublard : Character
{

    private bool isHidden { get; set; }

    public Roublard(Vector3 position, int maxHealth, int damage, bool isBlue) : base(position,  maxHealth, damage, isBlue)
    {
        isHidden = false;
        characterType = type.roublard;
    }

    public override void move(Vector3 target)
    {
        int moveRange = 1;
        if (isHidden)
        {
            moveRange = 2;
        }
    }

    public override void attack()
    {
        if (isHidden)
        {
            isHidden = false;
        }

    }

    // Trap
    public override void skill1()
    {
        if (isHidden)
        {
            isHidden = false;
        }
    }

    // Stealth
    public override void skill2()
    {
        if (isHidden)
        {
            isHidden = false;
        }
    }

}