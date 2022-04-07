using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public List<GameObject> colliding;

    private void OnTriggerEnter(Collider other)
    {
        colliding.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        colliding.Remove(other.gameObject);
    }

    private void fireTick() 
    {
        foreach (GameObject gameObject in colliding)
        {
            if (gameObject.GetComponent<Character>() != null)
            {
                gameObject.GetComponent<Character>().takeDamage(null, 25);
            }
        }
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
