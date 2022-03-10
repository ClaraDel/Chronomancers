using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager
{

    int dmg;
    private Character character;
    Afficheur afficheur;
    private Zone zone;

    public AttackManager (Vector3[] positions, int damage, Character character, int porteeMin, int porteeMax)
    {
        this.dmg = damage;
        this.character = character;
        List<Vector3> listPositions = new List<Vector3>();
        for (int i = 0; i < positions.Length; i++)
        {
            listPositions.Add(positions[i]);
        }
        zone = new Zone(character.gameObject.transform.position, porteeMin, porteeMax, listPositions);
    }


    public void endAtk()
    {
        afficheur.endDisplay();
    }


    public void setupAttack(Vector3 playerPosition)
    {
        afficheur = Afficheur.create(zone);
        afficheur.display();
    }

    public void attackTiles(List<Vector3> positions, Vector3 cursorPosition)
    {
        for (int i = positions.Count - 1; i >= 0; i--)
        {
            attackTile(positions[i] + cursorPosition);
        }
    }
    
    public void attackTile(Vector3 position)
    {
        if (character.isAlive())
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(position, Vector3.forward);

            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    Character target = hits[i].collider.gameObject.GetComponent<Character>();
                    target.takeDamage(character, dmg);
                }
                else
                {
                }

            }
        }
    }

    public bool applyAttack()
    {
        if (!afficheur.cursor.isValidPosition())
        {
            return false;
        }

        Vector3 cursorPos = afficheur.getCursorPosition();
        List<Vector3> zoneEffets = zone.getZoneEffets();

        TimeManager.instance.AddAction(() => attackTiles(zoneEffets, cursorPos));
        character.StartCoroutine(TimeManager.instance.PlayTick());

        return true;
    }

}
