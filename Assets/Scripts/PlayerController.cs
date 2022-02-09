using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public MoveManager moveManager;
    public int positionX;
    public int positionY;
    public bool isControllable;
    private Character character;
    private bool attackingProcess = false;


    // Start is called before the first frame update
    void Start()
    {
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        PlayerTarget.parent = null;
        isControllable = true;
        TimeManager.instance.NewCharacter(this);
        moveManager.AddResetPosition();
        character = gameObject.transform.GetComponent<Character>();
        character.init(100,50);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            attackingProcess = true;
            isControllable = false;
           
            if (character != null)
            {
                Debug.Log("attack");
                character.atk.setupAttack(gameObject.transform.position);
         
            }

        }
        if (attackingProcess)
        {
            if (Input.GetKey(KeyCode.W))
            {
                character.atk.selectAttack("W");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                character.atk.selectAttack("A");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                character.atk.selectAttack("S");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                character.atk.selectAttack("D");
            }
            else
            {

            }
        }
        if (isControllable && !TimeManager.instance.isPlaying)
        {
            if (Vector2.Distance(transform.position, PlayerTarget.position) == 0f)
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
                    TimeManager.instance.PlayTick();
                }
            }
        }
    }
}
