using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance { get; set;}
    int nbSlots = 5;
    int team = 1;
    private Dictionary<int,List<GameObject>> prefabPlayers;
    [SerializeField] GameObject[] lobbies;
    [SerializeField] GameObject [] confirmButtons;

    private void showButtonEnd()
    {
        confirmButtons[team - 1].SetActive(true);
    }


    public void confirmSelectionTeam1()
    {
        lobbies[0].SetActive(false);
        lobbies[1].SetActive(true);
        team += 1;

    }

    public void confirmSelectionTeam2()
    {
        lobbies[1].SetActive(false);
    }
    public void Replace(GameObject oldGo, GameObject newGo)
    {
        for(int i = 0; i < prefabPlayers[team].Count; i++)
        {
            if(prefabPlayers[team][i] == oldGo)
            {
                prefabPlayers[team][i] = newGo;
            }
        }
    }
    private void Awake()
    {
        instance = this;
        prefabPlayers = new Dictionary<int, List<GameObject>>();
    }

    public void Add(GameObject player)
    {
        if (!prefabPlayers.ContainsKey(team)){
            prefabPlayers.Add(team, new List<GameObject>());
        }
        prefabPlayers[team].Add(player);
        if(prefabPlayers[team].Count == nbSlots)
        {
            showButtonEnd();
        }
    }

}
