using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{

    public PlayerController entity;
    public int positionSpawnX = 22;
    public int positionSpawnY = 1;

    public void AddMove(float horizontalDirection, float verticalDirection)
    {
        TimeManager.instance.AddAction(() => this.StartCoroutine(Move(horizontalDirection, verticalDirection)));
        StartCoroutine(TimeManager.instance.PlayTick());
    }

    public IEnumerator Move(float horizontalDirection, float verticalDirection)
    {
        entity.PlayerTarget.Translate(new Vector2(horizontalDirection, verticalDirection));
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
        entity.PlayerTarget.position = new Vector3(positionSpawnX, positionSpawnY, 0);
        entity.transform.position = new Vector3(positionSpawnX, positionSpawnY, 0);
    }


}
