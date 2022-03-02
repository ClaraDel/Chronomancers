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


    private Character character;
    private bool attackingProcess = false;

    Afficheur a;


    public void setId(int id)
    {
        this.id = id;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerTarget.parent = null;
        isControllable = true;
        moveManager.AddResetPosition();
        TimeManager.instance.AddNewCharacter(this);
        
        character = gameObject.transform.GetComponent<Roublard>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(TimeManager.currentTick == TimeManager.maxTick)
        {
            //character.reset();
            moveManager.AddResetPosition();
        }
        
        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (!attackingProcess && character != null)
                {
                    character.attack();
                    attackingProcess = true;
                }
            }
            
            if (attackingProcess && Input.GetKeyUp(KeyCode.Return))
            {
               
                character.getAtk().applyAttack();
                character.endAtk();
                attackingProcess = false;*/

            } else if (attackingProcess && Input.GetKeyUp(KeyCode.Escape))
            {
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
                    character.wait();
                }
            }
        }
    }
}
