using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    private string stringVainqueur ;
    [SerializeField] private Animator mageRouge ;
    [SerializeField] private Animator mageBleu ;

    // Start is called before the first frame update
    void Start()
    {
        if(ScoreManager.instance.getTeamWinner() == 0)
        {
            stringVainqueur = "Player 1 est vainqueur !";
        } else if(ScoreManager.instance.getTeamWinner() == 1)
        {
            stringVainqueur = "Player 2 est vainqueur !";
        } else
        {
            stringVainqueur = "Egalité des deux équipes !";
        }
        GameObject.Find("NomVainqueur").GetComponent<TextMeshProUGUI>().text = stringVainqueur;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
