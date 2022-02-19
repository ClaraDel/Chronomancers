using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private GameObject fill;
    private float normalAttackDamage;
    public Attack atk;
    public Sprite ghostSprite;
    private Sprite characterSprite;
    private bool alive = true;
    private int numberTickLeft;
    private int team = 0; //vaut 0 s'il est dans l'équipe rouche et 1 s'il est dans l'équipe bleu


    /***********************************************getter********************************************/
    public int getTeam()
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

    private Attack getAttack()
    {
        return this.atk;
    }

    public bool isAlive()
    {
        return alive;
    }

    /***********************************************setter********************************************/
    public void setTeam(int team)
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

    private void setAttaque (Attack atk)
    {
        this.atk = atk;
    }

    private void setAlive(bool alive)
    {
        this.alive = alive;
    }

    private void setHealth(float health)
    {
        this.health = health;
    }


    //int pour savoir combien de tour il reste à cast

    public static Character create(Vector3 position, float health, int damage)
    {
        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        character.init(health, damage);
        character.setHealth(health);
        character.setNormalAttackDamage(damage);
        return character;
    }

    // à override
    public void init(float health, int damage)
    {
        maxHealth = (int)health;
        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
        , damage, this
            );
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        fill = GameObject.Find("Fill");
        healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
        healthBar.transform.GetComponent<Slider>().value = maxHealth;
        setHealth(health);
        if (!alive)
        {
            reset();
        }
        setTeam(ScoreManager.instance.getCurrentTeam()); //A MODIFIER ET VOIR AVEC NOMANINA
        if (getTeam() == 0)
        {
            fill.GetComponent<Image>().color = Color.red;
        }
        else if (getTeam() == 1)
        {
            fill.GetComponent<Image>().color = Color.blue;
        }
    }


    public void endAtk()

    {
        atk.endAtk();
        
        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
       , 50, this
           );
    }

   

    //à renommer en reset
    public void reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        healthBar.SetActive(true);
        alive = true;
    }


    public void die()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        healthBar.SetActive(false);
        alive = false;
    }

    public void takeDamage(int damage)
    {
        if (alive)
        {
            health = health - damage;
            healthBar.transform.GetComponent<Slider>().value = health;

            DamagePopup.create(damage, gameObject);
        }
        
    }
  

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && alive)
        {
            characterSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            die();
        }
       
    }
}
