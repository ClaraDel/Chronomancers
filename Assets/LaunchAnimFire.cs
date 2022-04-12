using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAnimFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = transform.GetComponent<Animator>();
        animator.Play("FireExplodePyromancien");
        StartCoroutine(WaitAndDie());
    }

    IEnumerator WaitAndDie()  
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
