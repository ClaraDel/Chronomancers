using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    private string stringVainqueur ;
    [SerializeField] private GameObject mageRouge ;
    [SerializeField] private GameObject mageBleu ;
    [SerializeField] private GameObject NomVainqueur;

    // Start is called before the first frame update
    public void ResultUI()
    {
        if(ScoreManager.instance.getTeamWinner() == 0)
        {
            stringVainqueur = "Player 1 est vainqueur !";
            mageRouge.GetComponent<Animator>().Play("WizardRedAttack2");
            mageBleu.GetComponent<Animator>().Play("WizardBlueDeath");
            //GameObject.Find("NomVainqueur").GetComponent<TextMeshProUGUI>().color = new Color32(255, 81, 81, 255); ;
            NomVainqueur.GetComponent<TextMeshProUGUI>().color = new Color32(255, 81, 81, 255); 

        } else if(ScoreManager.instance.getTeamWinner() == 1)
        {
            stringVainqueur = "Player 2 est vainqueur !";
            mageRouge.GetComponent<Animator>().Play("WizardRedDeath");
            mageBleu.GetComponent<Animator>().Play("WizardBlueAttack2");
            //GameObject.Find("NomVainqueur").GetComponent<TextMeshProUGUI>().color = new Color32(82, 119, 255, 255); ;
            NomVainqueur.GetComponent<TextMeshProUGUI>().color = new Color32(255, 81, 81, 255);
        } else
        {
            stringVainqueur = "Egalité des deux équipes !";
            mageRouge.GetComponent<Animator>().Play("WizardRedAttack2");
            mageBleu.GetComponent<Animator>().Play("WizardBlueAttack2");
            //GameObject.Find("NomVainqueur").GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255); ;
            NomVainqueur.GetComponent<TextMeshProUGUI>().color = new Color32(255, 81, 81, 255);
        }
        NomVainqueur.GetComponent<TextMeshProUGUI>().text = stringVainqueur;

    }
}
