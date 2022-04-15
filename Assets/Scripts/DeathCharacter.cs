using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Animator animator = transform.GetComponent<Animator>();
        if (animator.name.Contains("DeathPaladin"))
            animator.Play("deathPaladin");
        else if (animator.name.Contains("DeathRoublard"))
            animator.Play("deathRoublard");
            //Debug.Log("in death roublard");
        else if (animator.name.Contains("DeathPyromancien"))
            animator.Play("DisappearPyromancien");
        else if (animator.name.Contains("DeathBarbare"))
            animator.Play("deathBarbare");
        else if (animator.name.Contains("DeathRanger"))
            animator.Play("DeathRanger");
        else
            Debug.Log("Aucun perso ne correspond, Animator.parameters = "+ animator.parameters+ animator.name);
    }


}
