using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform PlayerTarget;
    public bool isControllable;
    private Character character;
    private bool attackingProcess = false;

   


    // Start is called before the first frame update
    void Start()
    {
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
                        character.moveH();
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                    {
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
