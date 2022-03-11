using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //s�parer affichage et attaque, pas forc�ment affichage d'attaque mais affichage en g�n�ral

    public Character character;
    public Zone zone;
    public int dmg;

    public Attack(Character owner, Zone zone, int damage) {
        this.character = owner;
        this.zone = zone;
        this.dmg = damage;
    }
}
