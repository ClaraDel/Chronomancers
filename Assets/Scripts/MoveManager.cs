using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public MoveManager(PlayerController entity){
        this.entity = entity;
    }

    public PlayerController entity;

    public void AddMove(float horizontalDirection, float verticalDirection){
        TimeManager.instance.AddAction(() => this.Move(horizontalDirection, verticalDirection));
        TimeManager.instance.playTick();
    }

    public void Move(float horizontalDirection, float verticalDirection){
        entity.PlayerTarget.Translate(new Vector2(horizontalDirection, verticalDirection));
        entity.positionX += Mathf.RoundToInt(horizontalDirection);
        entity.positionY += Mathf.RoundToInt(verticalDirection);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entity.transform.position = Vector2.MoveTowards(entity.transform.position, entity.PlayerTarget.position, entity.moveSpeed * Time.deltaTime);
    }
}
