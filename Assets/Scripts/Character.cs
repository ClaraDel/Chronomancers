using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private float normalAttackDamage;
    public AttackTest atk;
    public Sprite ghostSprite;

    public static Character create(Vector3 position, float health, float damage)
    {
        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        character.initHealth(health);
        character.setHealth(health);
        character.setNormalAttackDamage(damage);
        return character;
    }

    public void initHealth(float health)
    {
        maxHealth = (int) health;
        atk = new AttackTest();
        healthBar = (gameObject.transform.Find("Canvas")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
        healthBar.transform.GetComponent<Slider>().value = maxHealth;
    }

    public void setHealth(float health)
    {
        this.health = health;
        healthBar.transform.GetComponent<Slider>().value = health;
    }

    public void die()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    public void simulateDamage(int damage)
    {
        health = health - damage;
        healthBar.transform.GetComponent<Slider>().value = health;
    }


    public void setNormalAttackDamage(float damage)
    {
        this.normalAttackDamage = damage;
    }


    
    public void setup()
    {

    }

    // Start is called before the first frame update
    void Awake()
    {
        /*healthBar = (gameObject.transform.Find("Canvas")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
        healthBar.transform.GetComponent<Slider>().value = maxHealth;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            die();
        }
        
    }
}
