using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    int dmg;
    private Character character;
    Afficheur afficheur;
    private Zone zone;

    public static AttackManager create(Vector3[] positions, int damage, Character character, int porteeMin, int porteeMax)
    {
        Transform attackManagerTransform = Instantiate(GameAssets.i.attackManager);
        AttackManager attackManager = attackManagerTransform.GetComponent<AttackManager>();
        attackManager.init(positions, damage, character, porteeMin, porteeMax);
        return attackManager;
    }

    public void init(Vector3[] positions, int damage, Character character, int porteeMin, int porteeMax)
    {
        this.dmg = damage;
        this.character = character;
        List<Vector3> listPositions = new List<Vector3>();
        for(int i = 0; i < positions.Length; i++)
        {
            listPositions.Add(positions[i]);
        }
        zone = new Zone(character.gameObject.transform.position, porteeMin, porteeMax, listPositions);
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
        afficheur.endDisplay();
        Destroy(gameObject);
    }


    public void setupAttack(Vector3 playerPosition)
    {
        afficheur = Afficheur.create(zone);
        afficheur.display();
    }

    public void applyAttack(Vector3[] positions)
    {   
        if (character.isAlive())
        {
            Vector3 position1 = positions[0];
            Vector3 position2 = positions[1];
            Vector3 dir2target = -position1 + position2;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, dir2target);

            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    Character target = hits[i].collider.gameObject.GetComponent<Character>();
                    target.takeDamage(character, damage);
                }

            }
            /*
            RaycastHit2D hit = Physics2D.Raycast(position1,
                dir2target);

            if (hit.collider != null)
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                target.takeDamage(character, damage);
            }*/
        }
    }

    public bool applyAttack()
    {
        if (!afficheur.cursor.isValidPosition())
        {
            return false;
        }

        Vector3 cursorPos = afficheur.getCursorPosition();
        Vector3 playerPos = character.gameObject.transform.position;

        TimeManager.instance.AddAction(() => applyAttack(new[] { new Vector3(0,0,10), cursorPos }));
        character.StartCoroutine(TimeManager.instance.PlayTick());

        return true;
    }

}
