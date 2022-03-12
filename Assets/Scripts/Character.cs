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
    [SerializeField] private GameObject fill;

    public int normalAttackDamage;
    public Zone zoneBasicAttack;
    public GameObject cursor;

    public Sprite ghostSprite;
    public Sprite characterSprite;

    public bool alive;
    public bool isBlue;
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
    public int coolDownSkill2;

    public bool shielded;
    public int shieldDuration;

    public MoveManager moveManager;

    private int team ; //vaut 0 s'il est dans l'�quipe rouche et 1 s'il est dans l'�quipe bleu


    public void init(int maxHealth, int damage, bool isBlue)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;

        this.normalAttackDamage = damage;

        this.alive = true;
        this.isBlue = isBlue;

        GameObject rangeArea = gameObject.transform.Find("BasicAttackRange").gameObject;
        Debug.Log(rangeArea.transform.childCount);

        GameObject effectArea = gameObject.transform.Find("Cursor").Find("BasicAttackEffet").gameObject;
        Debug.Log(effectArea.transform.childCount);

        this.cursor = gameObject.transform.Find("Cursor").gameObject;


        this.zoneBasicAttack = gameObject.AddComponent<Zone>();
        this.zoneBasicAttack.init(rangeArea, effectArea);

        rangeArea.SetActive(false);
        effectArea.SetActive(false);

        this.cursor.SetActive(false);

        this.moveAction = false;

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

        this.castingTicks = 0;

        this.castingSkill1 = false;
        this.coolDownSkill1 = 0;
        this.castingSkill2 = false;
        this.coolDownSkill2 = 0;

        this.shielded = false;
        this.shieldDuration = 0;
    }
    public type getType() { return characterType; }
    public bool isAlive() { return alive; }
    public int getCastingTicks() { return castingTicks; }
    public bool isMoveAction() { return moveAction; }
    public int getTeam() { return this.team; }
    public void setTeam(int team) { this.team = team; }

    public virtual void reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        health = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
        healthBar.SetActive(true);
        alive = true;
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
        Debug.Log(damage);
        if (alive)
        {
            if (shielded)
            {
                shielded = false;
                shieldDuration = 0;
            }
            else
            {
                if (attacker.getType() == type.roublard && castingTicks > 0)
                {
                    damage = 2 * damage;
                }
                health = health - damage;
                DamagePopup.create(damage, gameObject);
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

    public virtual void setUpAttack()
    {
        this.zoneBasicAttack.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        gameObject.transform.Find("Cursor").GetComponent<CursorManager>().setUp(zoneBasicAttack);
    }

    public virtual void addAttack()
    {
        GameObject Cursor = gameObject.transform.Find("Cursor").gameObject;
        AttackManager.instance.addAttack(this, Cursor, zoneBasicAttack, normalAttackDamage);

        this.zoneBasicAttack.getZoneCiblable().SetActive(false);
        Cursor.GetComponent<CursorManager>().gameObject.SetActive(false);
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

    public void heal(int pvs)
    {
        health += pvs;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void shield(int nbturns)
    {
        shielded = true;
        shieldDuration = nbturns;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Start()
    {
        moveManager = gameObject.GetComponent<MoveManager>();
        moveManager.AddResetPosition();
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
    }
}
