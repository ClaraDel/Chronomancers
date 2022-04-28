using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        if (transform.name.Contains("FireBall"))
        {
            animator.Play("FirePyromancien");
        }
        
    }

    public IEnumerator Move(Vector3 target)
    {
        while (Vector2.Distance(transform.position, target) != 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,  Time.deltaTime * 5);
            yield return null;
        }
        animator.Play("FireExplodePyromancienCentre");
        StartCoroutine(WaitAndDie());
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
