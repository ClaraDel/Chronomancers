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

    public void setSelf(int castTime){
        int delay = Mathf.Min(TimeManager.maxTick - (TimeManager.instance.currentTick + castTime), 6);
        for (int i = 0; i < delay; i++)
        {
            TimeManager.instance.AddFutureAction(() => fireTick(), castTime + i);   
        }
        TimeManager.instance.AddFutureAction(() => gameObject.SetActive(false), castTime + delay);
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
