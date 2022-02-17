using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{

    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private float normalAttackDamage;
    public AttackTest atk;
    public Sprite ghostSprite;
    private Sprite characterSprite;
    private bool alive = true;
    private int numberTickLeft;
    private bool team;

    /***********************************************getter********************************************/
    private bool getTeam()
    {
        return this.team;
    }

    private float getHealth()
    {
        return this.health;
    }

    private float getMaxHealth()
    {
        return this.maxHealth;
    }

    private GameObject getHealthBar()
    {
        return this.healthBar;
    }

    private float getNormalAttackDamage()
    {
        return normalAttackDamage;
    }

    private Sprite getGhostSprite()
    {
        return this.ghostSprite;
    }

    private Sprite getCharacterSprite() {
        return this.characterSprite;
    }

    /***********************************************setter********************************************/
    private void setTeam(bool team)
    {
        this.team = team;
    }

    private void getHealth(float health)
    {
        this.health = health;
    }

    private void getMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    private void getHealthBar(GameObject healthBar)
    {
        this.healthBar = healthBar;
    }

    private void setNormalAttackDamage(float normalAttackDamage)
    {
        this.normalAttackDamage = normalAttackDamage;
    }

    private void setGhostSprite(Sprite ghostSprite)
    {
        this.ghostSprite = ghostSprite;
    }

    private void setCharacterSprite(Sprite characterSprite)
    {
        this.characterSprite = characterSprite;
    }


    //int pour savoir combien de tour il reste � cast

    /*public static Character create(Vector3 position, float health, int damage)
    {
        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        character.init(health, damage);
        character.setHealth(health);
        character.setNormalAttackDamage(damage);
        return character;
    }*/

    // � override
    public abstract void init(float health, int damage);
    //{
    /*maxHealth = (int) health;
    atk = new AttackTest(new[] { 
        new Vector3 { x = 1, y = 0, z = 0 }, 
        new Vector3 { x = 2, y = 0, z = 0 } }
    ,50,this
        );
    healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
    healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
    healthBar.transform.GetComponent<Slider>().value = maxHealth;
    setHealth(health);
    if (!alive)
    {
        revive();
    }*/
    //}


    public abstract void endAtk();

    /*{
        atk.endAtk();
        
        atk = new AttackTest(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
       , 50, this
           );
    }*/

    //� renommer en reset
    public abstract void reset();

    //{
    /*gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
    healthBar.SetActive(true);
    alive = true;*/
    //}

    /*
    public void setHealth(float health)
    {
        this.health = health;
        healthBar.transform.GetComponent<Slider>().value = health;
    }*/

    public abstract void die();
    /*{
        gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        healthBar.SetActive(false);
        alive = false;
    }*/

    public abstract void takeDamage(int damage);

    /*{
        if (alive)
        {
            health = health - damage;
            healthBar.transform.GetComponent<Slider>().value = health;

            DamagePopup.create(damage, gameObject);
        }
        
    }*/

    public bool isAlive()
    {
        return alive;
    }


  

    // Update is called once per frame
    /*void Update()
    {
        if(health <= 0 && alive)
        {
            characterSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            die();
        }
       
    }*/
}
