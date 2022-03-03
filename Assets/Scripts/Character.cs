using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public enum type
    {
        roublard,
        ranger,
        pyromancien,
        paladin,
        barbare
    }

    public int health;
    public int maxHealth;
    public GameObject healthBar;

    public int normalAttackDamage;
    public Attack atk;

    public Sprite ghostSprite;
    public Sprite characterSprite;

    public bool alive;
    public bool isBlue;
    public Vector3 position;
    public type characterType;

    public bool moveAction;

    public int castingTicks;

    public bool castingSkill1;
    public int skill1CastTime;
    public int skill1CoolDownTime;
    public int coolDownSkill1;

    public bool castingSkill2;
    public int skill2CastTime;
    public int skill2CoolDownTime;
    public int coolDownSkill2 { get; set; }

    public bool shielded { get; set; }
    public int shieldDuration { get; set; }

    public MoveManager moveManager;

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
        this.moveAction = false;

        this.castingTicks = 0;

        this.castingSkill1 = false;
        this.coolDownSkill1 = 0;
        this.castingSkill2 = false;
        this.coolDownSkill2 = 0;

        this.shielded = false;
        this.shieldDuration = 0;

        Transform characterTransform = Instantiate(GameAssets.i.pfCharacterTest, position, Quaternion.identity);
        Character character = characterTransform.GetComponent<Character>();
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
    }

    public Attack getAtk() { return atk; }

    public type getType() { return characterType; }

    public bool isAlive() { return alive; }
    
    public int getCastingTicks() { return castingTicks; }

    public bool isMoveAction() { return moveAction; }

    public virtual void reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        health = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
        healthBar.SetActive(true);
        alive = true;
        moveManager.AddResetPosition();
    }

    public void coolDowns()
    {
        if (coolDownSkill1 > 0)
        {
            coolDownSkill1--;
        }
        if (coolDownSkill2 > 0)
        {
            coolDownSkill2--;
        }
        if (shieldDuration > 0)
        {
            shieldDuration--;
        }
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
            if (shielded)
            {
                shielded = false;
                shieldDuration = 0;
            }
            else
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
    }

    public virtual void cast()
    {
        coolDowns();
        if (castingSkill1)
        {
            castingSkill1 = false;
            launchSkill1();
        }
        else if (castingSkill2)
        {
            castingSkill2 = false;
            launchSkill2();
        }
        castingTicks = 0;
    }

    public virtual void wait()
    {
        StartCoroutine(TimeManager.instance.PlayTick());
        coolDowns();
    }

    public virtual void casting()
    {
        castingTicks--;
        wait();
    }

    public virtual void moveH()
    {
        moveManager.AddMove(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
        coolDowns();
    }

    public virtual void moveV()
    {
        moveManager.AddMove(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
        coolDowns();
    }

    public virtual void attack() 
    {
        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 } }
       , normalAttackDamage, this
           );
        atk.setupAttack(position);
        coolDowns();
    }

    public virtual void castSkill1()
    {
        if (coolDownSkill1 == 0)
        {
            castingTicks = skill1CastTime;
            castingSkill1 = true;
        }
        coolDowns();
    }

    public virtual void launchSkill1()
    {
        coolDownSkill1 = skill1CoolDownTime;
        coolDowns();
    }

    public virtual void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            castingTicks = skill2CastTime;
            castingSkill2 = true;
        }
        coolDowns();
    }

    public virtual void launchSkill2()
    {
        coolDownSkill2 = skill2CoolDownTime;
        coolDowns();
    }

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
        coolDowns();
        atk.applyAttack();
        atk.endAtk();

        atk = new Attack(new[] {
            new Vector3 { x = 1, y = 0, z = 0 } }
       , 50, this
           );
    }

    public void heal(int pvs)
    {
        health += pvs;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void shield (int nbturns)
    {
        shielded = true;
        shieldDuration = nbturns;
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
