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



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Controller");
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
        if (pause.getIfPaused() || CharacterInfoPanel.instance.getIfPaused()) return;

        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (character.getCastingTicks() > 1)
            {
                character.casting();
            }
            else if (character.getCastingTicks() == 1)
            {
                character.cast();
            }
            else
            {
                if (character.isMoveAction())
                {
                    attackingProcess = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    if (!attackingProcess && character != null)
                    {
                        character.setUpAttack();
                        attackingProcess = true;
                    }
                }

                if (attackingProcess && Input.GetKeyUp(KeyCode.Return))
                {
                    character.addAttack();
                    attackingProcess = false;
                }

                else if (attackingProcess && Input.GetKeyUp(KeyCode.Escape))
                {
                    character.endAtk();
                    attackingProcess = false;
                }

                if (!attackingProcess && Vector2.Distance(transform.position, PlayerTarget.position) == 0f)
                {
                    
                    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f)
                    {
                        //Checks for wall collision
                        Vector3 start = new Vector3(PlayerTarget.position.x+0.5f, PlayerTarget.position.y+0.5f, 0f);
                        float sens = Mathf.Round(Input.GetAxisRaw("Horizontal"));
                        Vector3 dir = new Vector3(sens, 0f, 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        TimeManager.instance.AddAction(() => character.moveH(sens));
                        StartCoroutine(TimeManager.instance.PlayTick());
                        //character.moveH(sens);
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                    {
                        //Checks for wall collision
                        Vector3 start = new Vector3(PlayerTarget.position.x + 0.5f, PlayerTarget.position.y + 0.5f, 0f);
                        float sens = Mathf.Round(Input.GetAxisRaw("Vertical"));
                        Vector3 dir = new Vector3(0f, sens, 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        TimeManager.instance.AddAction(() => character.moveV(sens));
                        StartCoroutine(TimeManager.instance.PlayTick());
                        //character.moveV(sens);
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
