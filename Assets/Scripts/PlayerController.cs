using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public bool isControllable;
    public PauseToggle pause;

    public Character character;
    private bool attackingProcess = false;
    private bool castSkill1 = false;
    private bool castSkill2 = false;
    private bool canMove = true;

    void clearAtk()
    {
        attackingProcess = false;
        character.cancelAtk();
        castSkill1 = false;
        character.cancelSkill1();
        castSkill2 = false;
        character.cancelSkill2();
    }


    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.Find("PauseMenu").GetComponent<PauseToggle>();
        Debug.Log(pause);
        PlayerTarget.parent = null;
        isControllable = true;
        TimeManager.instance.AddNewCharacter(this);
        
        character = gameObject.transform.GetComponent<Character>();
        CharacterInfoPanel.instance.characterInfo = character.selfInfo;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeManager.currentTick == TimeManager.maxTick)
        {
            character.reset();
        }
        if (pause.getIfPaused() || CharacterInfoPanel.instance.getIfPaused()) return;

        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (character.getCastingTicks() >= 1)
            {
                character.wait();
            }
            else
            {
                canMove = !(attackingProcess || castSkill1 || castSkill2);

                if (character.isMoveAction())
                {
                    attackingProcess = false;
                    castSkill1 = false;
                    castSkill2 = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    if (!attackingProcess && character != null)
                    {
                        clearAtk();
                        character.setUpAttack();
                        attackingProcess = true;
                    }
                }

                if (Input.GetKeyUp(KeyCode.Alpha2) && character.coolDownSkill1 == 0)
                {
                    if (!castSkill1 && character != null)
                    {
                        clearAtk();
                        character.setUpSkill1();
                        castSkill1 = true;
                    }
                }

                if (Input.GetKeyUp(KeyCode.Alpha3) && character.coolDownSkill2 == 0)
                {
                    if (!castSkill2 && character != null)
                    {
                        clearAtk();
                        character.setUpSkill2();
                        castSkill2 = true;
                    }
                }

                if (Input.GetKeyUp(KeyCode.Return)) {
                    if (attackingProcess)
                    {
                        character.addAttack();
                        clearAtk();
                    }

                    if (castSkill1)
                    {
                        character.castSkill1();
                        clearAtk();
                    }

                    if (castSkill2)
                    {
                        character.castSkill2();
                        clearAtk();
                    }
                }

                if (Input.GetKeyUp(KeyCode.Backspace))
                {
                    clearAtk();
                }

                if (canMove && Vector2.Distance(transform.position, PlayerTarget.position) == 0f)
                {
                    
                    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f)
                    {
                        //Checks for wall collision
                        Vector3 start = new Vector3(PlayerTarget.position.x+0.5f, PlayerTarget.position.y+0.5f, 0f);
                        float sens = Mathf.Round(Input.GetAxisRaw("Horizontal"));
                        Vector3 dir = new Vector3(sens, 0f, 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        character.moveH(sens);
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                    {
                        //Checks for wall collision
                        Vector3 start = new Vector3(PlayerTarget.position.x + 0.5f, PlayerTarget.position.y + 0.5f, 0f);
                        float sens = Mathf.Round(Input.GetAxisRaw("Horizontal"));
                        Vector3 dir = new Vector3(0f, sens, 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        character.moveV(sens);
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {
                        character.wait();
                    }
                }
            }
        }
    }
}
