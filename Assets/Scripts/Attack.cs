using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attack
{
    int damage {get; }
    void applyAttack(GameObject player, Vector3 position);
    
}
