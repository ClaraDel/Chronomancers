using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public float health;
    private float maxHealth;
    private GameObject healthBar;
    private float normalAttackDamage;
    public AttackTest atk;
    public Sprite ghostSprite;
    private Sprite characterSprite;
    private bool isAlive = true;

    public static Character create(Vector3 position, float health, int damage)
    {
        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        character.init(health, damage);
        character.setHealth(health);
        character.setNormalAttackDamage(damage);
        return character;
    }

    public void init(float health, int damage)
    {
        maxHealth = (int) health;
        setNormalAttackDamage(damage);
        atk = new AttackTest(new[] { 
            new Vector3 { x = 1, y = 0, z = 0 }, 
            new Vector3 { x = 2, y = 0, z = 0 } }
        ,50,this
            );
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
        healthBar.transform.GetComponent<Slider>().value = maxHealth;
        setHealth(health);
        if (!isAlive)
        {
            revive();
        }
    }

    public void endAtk()
    {
        atk.endAtk();
        
        atk = new AttackTest(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
       , 50, this
           );
    }

    public void revive()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        healthBar.SetActive(true);
        isAlive = true;
    }

    public void setHealth(float health)
    {
        this.health = health;
        healthBar.transform.GetComponent<Slider>().value = health;
    }

    public void die()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        healthBar.SetActive(false);
        isAlive = false;
    }

    public void simulateDamage(int damage)
    {
        if (isAlive)
        {
            health = health - damage;
            healthBar.transform.GetComponent<Slider>().value = health;

            DamagePopup.create(damage, gameObject);
        }
        
    }

    public bool alive()
    {
        return isAlive;
    }


    public void setNormalAttackDamage(float damage)
    {
        this.normalAttackDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && isAlive)
        {
            characterSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            die();
        }
       
    }
}
