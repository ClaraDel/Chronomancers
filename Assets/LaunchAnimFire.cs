using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAnimFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = transform.GetComponent<Animator>();
        if(transform.name.Contains("Bomb"))
        {
            animator.Play("BombPyromancien");
            Debug.Log("in BombPyromancien");
        } else if (transform.name.Contains("Burst"))
        {
            animator.Play("FireExplodePyromancien");
            Debug.Log("in FireExplodePyromancien");
        } else
        {
            Debug.Log("no name corresponding for bomb or burst");
        }
        StartCoroutine(WaitAndDie());
    }

    IEnumerator WaitAndDie()  
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
