using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; set; }

    public GameObject UIScore1;
    public GameObject UIScore2;

    [Header("Score :")]
    public int scoreTeam1;
    public int scoreTeam2;
    [Space]
    [Header("Coordonnées des zones de contrôle :")]
    public int hauteurArea;
    public int largeurArea;
    public int coordXArea1; //positionX de la case en bas à gauche de l'area1
    public int coordYArea1; //positionY de la case en bas à gauche de l'area1
    public int coordXArea2; 
    public int coordYArea2; 

    private int currentTeam;

    void Awake()
    {
        instance = this;
        scoreTeam1 = 0;
        scoreTeam2 = 0;
    }

    public void CheckInControlArea(Character character, int posX, int posY)
    {
        if (posX >= coordXArea1 && posX < coordXArea1 + largeurArea && posY >= coordYArea1 && posY < coordYArea1 + hauteurArea
            || posX >= coordXArea2 && posX < coordXArea2 + largeurArea && posY >= coordYArea2 && posY < coordYArea2 + hauteurArea)
        {
            Debug.Log("Character from team " + character.getTeam() + " is in area !");
            UpdateScore(1, character.getTeam());
        }
        
    }

    void UpdateScore(int value, int team)
    {
        if(team == 0)
        {
            scoreTeam1 = scoreTeam1 >= 0 ? scoreTeam1 += value : 0;
        } else if (team == 1)
        {
            scoreTeam2 = scoreTeam2 >= 0 ? scoreTeam2 += value : 0;
        }
        //Pour plus tard potentiellement : créer un struct de team regroupant toutes les infos sur l'équipe (personnalisation)
    }

    public void ResetScore()
    {
        scoreTeam1 = 0;
        scoreTeam2 = 0;
    }

    public void SwitchTeam(int team)
    {
        currentTeam = team;
        Debug.Log("Team n." + currentTeam);
    }
        
        void Update()
    {
        UIScore1.GetComponent<TMPro.TextMeshProUGUI>().text = scoreTeam1.ToString();
        UIScore2.GetComponent<TMPro.TextMeshProUGUI>().text = scoreTeam2.ToString();
    }
    public int getCurrentTeam()
    {
        return this.currentTeam;
    }
}
