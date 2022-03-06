using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    //séparer affichage et attaque, pas forcément affichage d'attaque mais affichage en général

    public Vector3 playerPosition;
    public List<Vector3> positions;
    int dmg;
    private Character character;
    private int porteeMin;
    private int porteeMax;
    Afficheur a;



    public Attack(Vector3[] positions, int damage, Character character, int porteeMin, int porteeMax) {
        this.positions = new List<Vector3>();
        for(int i = 0; i < positions.Length; i++)
        {
            this.positions.Add(positions[i]);
        }
        this.dmg = damage;
        this.character = character;
        this.porteeMax = porteeMax;
        this.porteeMin = porteeMin;
    }

    public int damage
    {
        get
        {
            return dmg;
        }
    }

   

    public void endAtk()
    {
        a.endDisplay();
    }


    public void setupAttack(Vector3 playerPosition)
    {

        a = Afficheur.create(character.gameObject.transform.position, porteeMin, porteeMax, positions);
        a.display();
    }

    public void applyAttack(Vector3 [] positions)
    {
        if (character.isAlive())
        {
            Vector3 position1 = positions[0];
            Vector3 position2 = positions[1];
            Vector3 dir2target = -position1 + position2;
            float lenRay = Vector3.Distance(position1, position2);
            RaycastHit2D hit = Physics2D.Raycast(position1,
                dir2target, lenRay);

            if (hit.collider != null)
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                target.takeDamage(character, damage);
            }
        }    
    }

    public bool applyAttack()
    {
        if (!a.cursor.isValidPosition())
        {
            return false;
        }
        Vector3 cursorPos = a.getCursorPosition();
        TimeManager.instance.AddAction(() => applyAttack(new[] { character.gameObject.transform.position, cursorPos }));
        TimeManager.instance.PlayTick();
        return true;
    }
   
}
