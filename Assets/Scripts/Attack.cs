using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attack
{
    int damage {get; }

    Vector3 [] possibleAttackPositions { get;  }

    void setupAttack(Vector3 playerPosition);
    void applyAttack();
    
}
