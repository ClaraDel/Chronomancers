using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{

    private void fireTick() 
    {
        if(gameObject.activeSelf){
            AttackManager.instance.attackTile(null, transform.position, 25);
        }
    }

    public void setSelf(){
        int delay = Mathf.Min(TimeManager.maxTick - TimeManager.instance.currentTick, 6);
        TimeManager.instance.AddFutureAction(() => gameObject.SetActive(true), 1);
        for (int i = 1; i < delay-1; i++)
        {
            TimeManager.instance.AddFutureAction(() => fireTick(), i);   
        }
        TimeManager.instance.AddFutureAction(() => gameObject.SetActive(false), delay-1);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        TimeManager.instance.addAction(() => fireTick());
        TimeManager.instance.addFutureAction(() => fireTick(), 1);
        TimeManager.instance.addFutureAction(() => fireTick(), 2);
        TimeManager.instance.addFutureAction(() => fireTick(), 3);
        TimeManager.instance.addFutureAction(() => fireTick(), 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
