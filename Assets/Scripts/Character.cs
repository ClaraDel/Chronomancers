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
    
    public CharacterInfoScriptableObject selfInfo;
    public int health;
    public int maxHealth;
    public GameObject healthBar;
    [SerializeField] private GameObject fill;

    public int normalAttackDamage;
    public Zone zoneBasicAttack;
    public Zone zoneSkill1;
    public Zone zoneSkill2;
    public GameObject cursor;
    public Transform characterDie;

    public Sprite characterSprite;

    public bool alive;
    public bool isBlue;
    public type characterType;

    public bool moveAction;

    public int castingTicks;

    public bool castingSkill1;
    public int skill1CastTime;
    public int coolDownSkill1;
    public int maxCoolDownSkill1;

    public bool castingSkill2;
    public int skill2CastTime;
    public int coolDownSkill2;
    public int maxCoolDownSkill2;

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

        // Zone Basic Attack init
        GameObject rangeAreaBasicAttack = gameObject.transform.Find("BasicAttackRange").gameObject;
        GameObject effectAreaBasicAttack = cursor.transform.Find("BasicAttackEffet").gameObject;
        this.zoneBasicAttack = gameObject.AddComponent<Zone>();
        this.zoneBasicAttack.init(rangeAreaBasicAttack, effectAreaBasicAttack);
        rangeAreaBasicAttack.SetActive(false);
        effectAreaBasicAttack.SetActive(false);

        // Zone Skill 1 init
        GameObject rangeAreaSkill1 = gameObject.transform.Find("Skill1Range").gameObject;
        GameObject effectAreaSkill1 = cursor.transform.Find("Skill1Effet").gameObject;
        this.zoneSkill1 = gameObject.AddComponent<Zone>();
        this.zoneSkill1.init(rangeAreaSkill1, effectAreaSkill1);
        rangeAreaSkill1.SetActive(false);
        effectAreaSkill1.SetActive(false);

        // Zone Skill 2 init
        GameObject rangeAreaSkill2 = gameObject.transform.Find("Skill2Range").gameObject;
        GameObject effectAreaSkill2 = cursor.transform.Find("Skill2Effet").gameObject;
        this.zoneSkill2 = gameObject.AddComponent<Zone>();
        this.zoneSkill2.init(rangeAreaSkill2, effectAreaSkill2);
        rangeAreaSkill2.SetActive(false);
        effectAreaSkill2.SetActive(false);

        this.cursor = cursor.gameObject;
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
        gameObject.tag = "character";

        this.castingTicks = 0;
        this.castingSkill1 = false;
        this.coolDownSkill1 = 0;
        this.castingSkill2 = false;
        this.coolDownSkill2 = 0;

        this.shielded = false;
        this.shieldDuration = 0;

        this.cursor.GetComponent<CursorManager>().reset();
        this.cursor.SetActive(false);
    }

    // Basic movement methods
    public virtual void moveH()
    {
        moveManager.AddMove(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
    }

    public virtual void moveV()
    {
        moveManager.AddMove(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
    }

    public virtual void wait()
    {
        this.StartCoroutine(TimeManager.instance.PlayTick());
        if (castingTicks > 0)
        {
            castingTicks--;
        }
    }
    
    // taking Damage method
    public virtual void takeDamage(Character attacker, int damage)
    {
        Debug.Log("Take Damage : " + damage.ToString());
        if (alive)
        {
            if (shielded)
            {
                shielded = false;
                shieldDuration = 0;
            }
            else
            {
                if (attacker != null && attacker.getType() == type.roublard && castingTicks > 0)
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

    public virtual void die()
    {
        gameObject.tag = "dead";
        transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
        Instantiate(characterDie, transform.position, transform.rotation);
        health = 0;
        healthBar.transform.GetComponent<Slider>().value = health;
        healthBar.SetActive(false);
        alive = false;
        
    }

    public void shield(int nbturns)
    {
        shielded = true;
        shieldDuration = nbturns;
    }

    public void heal(int pvs)
    {
        health += pvs;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void moveH(float sens)
    {
        moveManager.AddMove(sens, 0);
    }

    public virtual void moveV(float sens)
    {
        moveManager.AddMove(0, sens);
    }

    // Basic attack methods
    public virtual void setUpAttack()
    {
        this.zoneBasicAttack.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUp(zoneBasicAttack);
    }

    public virtual void addAttack()
    {
        // AbilityTimer.instance.launchUIAbility(1);

        CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
        Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
        for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
        {
            positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
        }

        TimeManager.instance.AddAction(() => castAttack(positions, cursor.direction));

        this.cursor.GetComponent<CursorManager>().reset();
        this.cursor.SetActive(false);
        this.StartCoroutine(TimeManager.instance.PlayTick());
    }
    
    public virtual void castAttack(Vector3[] positions, CursorManager.directions direction)
    {
        AttackManager.instance.attackTiles(this, positions, normalAttackDamage);
    }

    public void cancelAtk()
    {
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    public virtual void coolDowns()
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


    public virtual void setUpSkill1()
    {
        this.zoneSkill1.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUp(zoneSkill1);
    }

    public virtual void castSkill1()
    {
        if (coolDownSkill1 == 0)
        {
            castingTicks = skill1CastTime - 1;
            castingSkill1 = true;
            coolDownSkill1 = maxCoolDownSkill1 + skill1CastTime;

            CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
            Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
            for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
            {
                positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
            }


            TimeManager.instance.AddFutureAction(() => launchSkill1(positions), skill1CastTime);
            for (int i = 0; i < skill1CastTime + 1; i++)
            {
                this.StartCoroutine(TimeManager.instance.PlayTick());
            }
        }
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    public virtual void launchSkill1(Vector3[] positions) { }

    public void cancelSkill1()
    {
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    public virtual void setUpSkill2()
    {
        this.zoneSkill2.getZoneCiblable().SetActive(true);
        cursor.SetActive(true);
        cursor.GetComponent<CursorManager>().setUp(zoneSkill2);
    }

    public virtual void castSkill2()
    {
        if (coolDownSkill2 == 0)
        {
            castingTicks = skill2CastTime - 1;
            castingSkill2 = true;
            coolDownSkill2 = maxCoolDownSkill2 + skill2CastTime;

            CursorManager cursor = gameObject.transform.Find("Cursor").GetComponent<CursorManager>();
            Vector3[] positions = new Vector3[cursor.activeZone.getTilesEffets().Count];
            for (int i = 0; i < cursor.activeZone.getTilesEffets().Count; i++)
            {
                positions[i] = cursor.activeZone.getTilesEffets()[i].transform.position;
            }


            TimeManager.instance.AddFutureAction(() => launchSkill2(positions), skill2CastTime);
            for (int i = 0; i < skill2CastTime + 1; i++)
            {
                this.StartCoroutine(TimeManager.instance.PlayTick());
            }
        }
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    public virtual void launchSkill2(Vector3[] positions) { }

    public void cancelSkill2()
    {
        cursor.GetComponent<CursorManager>().reset();
        cursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Start()
    {
        cursor = gameObject.transform.Find("Cursor").gameObject;
        moveManager = gameObject.GetComponent<MoveManager>();
        moveManager.AddResetPosition();
        healthBar = (gameObject.transform.Find("pfHealthBar")).Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.transform.GetComponent<Slider>().value = health;
    }
}
