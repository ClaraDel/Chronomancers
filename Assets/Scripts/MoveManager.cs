using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{

    public PlayerController entity;
    //public GameObject characterObject;
    //private PlayerController playerController;
    public int positionSpawnXTeam0 = 22;
    public int positionSpawnYTeam0 = 1;
    public int positionSpawnXTeam1 = 1;
    public int positionSpawnYTeam1 = 10;

    void Start()
    {
        //playerController = characterObject.GetComponent<PlayerController>();
    }

    public void AddMove(float horizontalDirection, float verticalDirection)
    {
        //TimeManager.instance.AddAction(() => this.StartCoroutine(Move(horizontalDirection, verticalDirection)));
        this.StartCoroutine(Move(horizontalDirection, verticalDirection));
    }

    // public void AddDash(float horizontalDirection, float verticalDirection)
    // {
    //     //TimeManager.instance.AddAction(() => this.StartCoroutine(Move(horizontalDirection, verticalDirection)));
    //     this.StartCoroutine(Move(horizontalDirection, verticalDirection));
    // }

    public IEnumerator Move(float horizontalDirection, float verticalDirection)
    {
        entity.PlayerTarget.Translate(new Vector2(horizontalDirection, verticalDirection));

        //gestion de l'order Layout
        int posX = (int)entity.transform.position.x + (int)horizontalDirection;
        int posY = (int)entity.transform.position.y + (int)verticalDirection;
        entity.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13 - posY;

        //int orderLayout = (int)entity.gameObject.transform.position.y + (int)verticalDirection;
        entity.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13 - posY;
        while (Vector2.Distance(transform.position, entity.PlayerTarget.position) != 0f)
        {
            entity.transform.position = Vector2.MoveTowards(entity.transform.position, entity.PlayerTarget.position, entity.moveSpeed * Time.deltaTime);
            yield return null;
        } 
    }

    public void AddResetPosition()
    {
        TimeManager.instance.AddAction(() => this.ResetPosition());
        StartCoroutine(TimeManager.instance.PlayTick());
    }

    public void ResetPosition()
    {
         entity.PlayerTarget.position = new Vector3(positionSpawnXTeam0, positionSpawnYTeam0, 0);
         transform.position = new Vector3(positionSpawnXTeam0, positionSpawnYTeam0, 0);
    }


}
