using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public MoveManager moveManager;
    public bool isControllable;
    private PauseToggle pause;

    private Character character;
    private bool attackingProcess = false;
    private bool attackSelected = false;
    private bool selectingAttackPos = false;

    Afficheur a;


    public void setId(int id)
    {
        this.id = id;
    }
    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.Find("PauseMenu").GetComponent<PauseToggle>();
        PlayerTarget.parent = null;
        isControllable = true;
        moveManager.AddResetPosition();
        TimeManager.instance.AddNewCharacter(this);
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
        if (pause.getIfPaused()) return;

        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (!attackingProcess && character != null)
                {
                    //character.atk.setupAttack(gameObject.transform.position);
                    a = Afficheur.create(gameObject.transform.position, 2, 3,
                        new List<Vector3>() { new Vector3(0,1,0), new Vector3(0,-1,0), new Vector3(0,0,0) }
                        );
                    a.display();
                }
                attackingProcess = true;
            }
            if (attackingProcess)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    a.rotateEffects();
                }
                /*
              
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
                }*/
                attackSelected = true;

            }

            if (attackingProcess && Input.GetKeyUp(KeyCode.Return))
            {
                a.endDisplay();
                attackSelected = false;
                attackingProcess = false;

                /*
                attackSelected = false;
                character.atk.applyAttack();
                character.endAtk();
                attackingProcess = false;*/

            } else if (attackingProcess && Input.GetKeyUp(KeyCode.Escape))
            {
                a.endDisplay();
                attackSelected = false;
                attackingProcess = false;

                /*
                attackSelected = false;
                character.endAtk();
                attackingProcess = false;*/

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

                    moveManager.AddMove(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                {
                    //Checks for wall collision
                    Vector3 start = new Vector3(PlayerTarget.position.x + 0.5f, PlayerTarget.position.y + 0.5f, 0f);
                    Vector3 dir = new Vector3(0f, Mathf.Round(Input.GetAxisRaw("Vertical")), 0f);
                    RaycastHit hit;
                    if (Physics.Raycast(start, dir, out hit, 1f) && hit.transform.tag == "Wall") return;

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
