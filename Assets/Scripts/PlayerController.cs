using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public MoveManager moveManager;
    public bool isControllable;


    private Character character;
    private bool attackingProcess = false;
    private bool attackSelected = false;
    private bool selectingAttackPos = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerTarget.parent = null;
        isControllable = true;
        TimeManager.instance.AddNewCharacter(this);
        moveManager.AddResetPosition();
        character = gameObject.transform.GetComponent<Character>();
        character.initialise(100,50);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(TimeManager.currentTick == TimeManager.maxTick)
        {
            character.init(100, 50);
        }*/
        
        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (!attackingProcess && character != null)
                {
                    character.atk.setupAttack(gameObject.transform.position);
                }
                attackingProcess = true;

            }
            if (attackingProcess)
            {
              
                if (Input.GetKeyUp(KeyCode.W))
                {
                    character.atk.selectAttack("W");
                    attackSelected = true;
                }
                else if (Input.GetKeyUp(KeyCode.A))
                {
                    character.atk.selectAttack("A");
                    attackSelected = true;
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    character.atk.selectAttack("S");
                    attackSelected = true;
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    character.atk.selectAttack("D");
                    attackSelected = true;
                }
                else
                {
                }
            }

            if (attackingProcess && Input.GetKeyUp(KeyCode.Return))
            {
               
                attackSelected = false;
                character.atk.applyAttack();
                character.endAtk();
                attackingProcess = false;

            } else if (attackingProcess && Input.GetKeyUp(KeyCode.Escape))
            {
                attackSelected = false;
                character.endAtk();
                attackingProcess = false;

            }
            if (!attackingProcess && Vector2.Distance(transform.position, PlayerTarget.position) == 0f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f)
                {
                    moveManager.AddMove(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                {
                    moveManager.AddMove(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(TimeManager.instance.PlayTick());
                }
            }
        }
    }
}
