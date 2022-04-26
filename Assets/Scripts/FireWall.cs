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
        TimeManager.instance.AddAction(() => gameObject.SetActive(true));
        for (int i = 0; i < delay-1; i++)
        {
            TimeManager.instance.AddFutureAction(() => fireTick(), i);   
        }
        TimeManager.instance.AddFutureAction(() => gameObject.SetActive(false), delay);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
