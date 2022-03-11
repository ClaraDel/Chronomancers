// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class Pyromancien : Character
// {
//     public bool attacking;

//     public Pyromancien(Vector3 position, bool isBlue) : base(position,  75, 50, isBlue)
//     {
//         characterType = type.pyromancien;
//         skill1CastTime = 4;
//         skill1CoolDownTime = 8;
//         skill2CastTime = 2;
//         skill2CoolDownTime = 10;
//         attacking = false;
//     }

//     public override void cast()
//     {
//         if (attacking)
//         {
//             attacking = false;
//             launchAttack();
//         }
//         base.cast();
//     }

//     public override void attack()
//     {
//         castingTicks = 1;
//     }

//     public void launchAttack()
//     {
//         castingTicks = 1;
//         atk = new AttackManager(new[] {
//             new Vector3 { x = 0, y = 0, z = 0 } }, normalAttackDamage, this, 1, 1);
//         atk.setupAttack(position);
//         base.coolDowns();
//     }

//     // Explosion
//     public override void launchSkill1()
//     {
//         atk = new AttackManager(new[] {
//             new Vector3 { x = 0, y = 0, z = 0 } }, normalAttackDamage, this, 1, 1);
//         atk.setupAttack(position);
        
//         base.launchSkill1();
//     }

//     // Stealth
//     public override void launchSkill2()
//     {
//         // Creer objet mur de feu et le faire spawner
//         base.launchSkill2();
//     }

// }