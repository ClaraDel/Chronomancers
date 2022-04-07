using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Animator animator = transform.GetComponent<Animator>();
        if (animator.name == "DeathPaladin")
            animator.Play("deathPaladin");
        else if (animator.name == "DeathRoublard")
            animator.Play("deathPaladin");
        else if (animator.name == "DeathPyromancien")
            animator.Play("deathPyromancien");
        else if (animator.name == "DeathBarbare")
            animator.Play("deathBarbare");
        else if (animator.name == "DeathRanger")
            animator.Play("deathRanger");
        else
            print("Aucun perso ne correspond");
    }


}
