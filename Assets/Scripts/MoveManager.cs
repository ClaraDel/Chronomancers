using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public MoveManager(PlayerController entity)
    {
        this.entity = entity;
    }

    public PlayerController entity;

    public void AddMove(float horizontalDirection, float verticalDirection)
    {
        TimeManager.instance.AddAction(() => this.StartCoroutine(Move(horizontalDirection, verticalDirection)));
        TimeManager.instance.PlayTick();
    }

    public IEnumerator Move(float horizontalDirection, float verticalDirection)
    {
        entity.PlayerTarget.Translate(new Vector2(horizontalDirection, verticalDirection));
        entity.positionX += Mathf.RoundToInt(horizontalDirection);
        entity.positionY += Mathf.RoundToInt(verticalDirection);
        while (Vector2.Distance(transform.position, entity.PlayerTarget.position) != 0f)
        {
            entity.transform.position = Vector2.MoveTowards(entity.transform.position, entity.PlayerTarget.position, entity.moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void AddResetPosition()
    {
        TimeManager.instance.AddAction(() => this.ResetPosition());
        TimeManager.instance.PlayTick();
    }

    public void ResetPosition()
    {
        entity.positionX = 0;
        entity.positionY = 0;
        entity.PlayerTarget.position = new Vector3(0, 0, 0);
        entity.transform.position = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
