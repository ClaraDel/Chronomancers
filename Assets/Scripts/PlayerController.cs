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
        TimeManager.instance.NewCharacter(this);
        moveManager.AddResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
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
