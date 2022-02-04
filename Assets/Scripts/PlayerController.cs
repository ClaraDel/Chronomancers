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


    // Start is called before the first frame update
    void Start()
    {
        positionX = (int)Mathf.Floor(transform.position.x);
        positionY = (int)Mathf.Floor(transform.position.y);
        PlayerTarget.parent = null;
        isControllable = true;
        // moveManager = new MoveManager(this);
        TimeManager.instance.NewCharacter(this);
        TimeManager.instance.AddAction(() => ResetPosition());
        TimeManager.instance.playTick();
    }

    public void ResetPosition()
    {
        positionX = 0;
        positionY = 0;
        PlayerTarget.position = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, PlayerTarget.position, moveSpeed * Time.deltaTime);
        if (isControllable)
        {
            if (Vector2.Distance(transform.position, PlayerTarget.position) == 0f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f)
                {
                    moveManager.AddMove(Mathf.Round(Input.GetAxisRaw("Horizontal")), 0);
                    // PlayerTarget.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
                {
                    moveManager.AddMove(0, Mathf.Round(Input.GetAxisRaw("Vertical")));
                    // PlayerTarget.Translate(new Vector2(0, Input.GetAxisRaw("Vertical")));
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    TimeManager.instance.playTick();
                }
            }
        }
    }
}
