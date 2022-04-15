using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Character>().alive)
        {
            other.gameObject.GetComponent<Character>().takeDamage(null, 50);
            Destroy(this.gameObject);
        }
    }
}
