using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public enum type
    {
        roublard
    }

    protected int health { get; set; }
    protected int maxHealth { get; set; }
    protected GameObject healthBar { get; set; }
    protected int normalAttackDamage { get; set; }
    protected Attack atk;
    public Sprite ghostSprite { get; }
    protected Sprite characterSprite { get; }
    protected bool alive; 
    protected int numberTickLeft { get; set; }
    protected bool isBlue { get; set; }
    protected Vector3 position { get; set; }
    protected type characterType;

    public Character (Vector3 position, int maxHealth, int damage, bool isBlue) 
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.normalAttackDamage = damage;
        this.alive = true;
        this.isBlue = isBlue;
        this.position = position;
        this.atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
       , 50, this
           );

        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
    }

    public Attack getAtk() { return atk; }

    public virtual void reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        health = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
        healthBar.SetActive(true);
        alive = true;
    }


    public virtual void die()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        health = 0;
        healthBar.transform.GetComponent<Slider>().value = health;
        healthBar.SetActive(false);
        alive = false;
    }

    public virtual void takeDamage(Character attacker, int damage)
    {
        if (alive)
        {
            if (attacker.getType() == type.roublard)
            {
                int dmg = 2 * damage;
                health = health - dmg;
                DamagePopup.create(dmg, gameObject);
            }
            else
            {
                health = health - damage;
                DamagePopup.create(damage, gameObject);
            }            
            if (health > 0)
            {
                healthBar.transform.GetComponent<Slider>().value = health;
            } 
            else
            {
                die();
            }
        }
        
    }

    public virtual void wait()
    {
        StartCoroutine(TimeManager.instance.PlayTick());
    }

    public virtual void move(Vector3 target)
    {

    }

    public virtual void attack() 
    {
        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
       , 50, this
           );
        atk.setupAttack(position);
    }

    public virtual void skill1() { }

    public virtual void skill2() { }

    public type getType() { return characterType; }

    public bool isAlive() { return alive; }

    private void setAlive(bool alive)
    {
        this.alive = alive;
    }

    private void setHealth(float health)
    {
        this.health = health;
    }


    //int pour savoir combien de tour il reste � cast

    public static Character create(Vector3 position, float health, int damage)
    {
        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        character.initialise(health, damage);
        character.setHealth(health);
        character.setNormalAttackDamage(damage);
        return character;
    }

    // � override
    public void initialise(float health, int damage)
    {
        setTeam(ScoreManager.instance.getCurrentTeam()); //A MODIFIER ET VOIR AVEC NOMANINA
        //fill = GameObject.Find("Fill");
        if (getTeam() == 0)
        {
            fill.GetComponent<Image>().color = Color.red;
        }
        else if (getTeam() == 1)
        {
            fill.GetComponent<Image>().color = Color.blue;
        }
        Debug.Log("init of character order" + gameObject.GetComponent<PlayerController>().id + " from team " + getTeam());
        maxHealth = (int)health;
        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 },
            new Vector3 { x = 2, y = 0, z = 0 } }
        , damage, this
            );
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = this.maxHealth;
        healthBar.transform.GetComponent<Slider>().value = maxHealth;
        setHealth(health);
        if (!alive)
        {
            reset();
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

   

    //� renommer en reset
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

            DamagePopup.create(-damage, gameObject);
        }
        
    }
  

    // Update is called once per frame
    void Update()
    {       
    }
}
