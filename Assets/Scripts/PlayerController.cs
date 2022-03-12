using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public bool isControllable;
    private PauseToggle pause;

    private Character character;
    private bool attackingProcess = false;



    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.Find("PauseMenu").GetComponent<PauseToggle>();
        PlayerTarget.parent = null;
        isControllable = true;
        TimeManager.instance.AddNewCharacter(this);
        
        character = gameObject.transform.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeManager.currentTick == TimeManager.maxTick)
        {
            character.reset();
        }
        if (pause.getIfPaused()) return;

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
                        Vector3 dir = new Vector3(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0f, 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        character.moveH();
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                    {
                        //Checks for wall collision
                        Vector3 start = new Vector3(PlayerTarget.position.x + 0.5f, PlayerTarget.position.y + 0.5f, 0f);
                        Vector3 dir = new Vector3(0f, Mathf.Round(Input.GetAxisRaw("Vertical")), 0f);
                        RaycastHit hit;
                        if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;
                        character.moveV();
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
