using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Character>().alive && other.gameObject.GetComponent<Character>() != null)
        {
            transform.GetComponent<Animator>().Play("Trap");
            other.gameObject.GetComponent<Character>().takeDamage(null, 50);
            StartCoroutine(WaitAndDie());
        }
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
