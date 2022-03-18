using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance { get; set;}

    public int nbEquipedSlot = 0;
    public int team = 1;
    bool full = false;

    [SerializeField] int nbMax = 5;
    [SerializeField] GameObject[] lobbies;

    private Dictionary<int, List<GameObject>> prefabPlayers;

    public GameObject [] confirmButtons;
    public GameObject lastFilledSlot;
    public GameObject lastChosenCharacter;

    public virtual void showButtonEnd()
    {
        confirmButtons[team - 1].SetActive(true);
    }

    public bool isFull()
    {
        return full;
    }

    public void Save(GameObject lastFilledSlot, GameObject lastChosenCharacter)
    {
        this.lastFilledSlot = lastFilledSlot;
        this.lastChosenCharacter = lastChosenCharacter;
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

    public void RemoveObjectFromLastSlot()
    {
        //remove prefab from the list of prefabs
        for (int i = 0; i < prefabPlayers[team].Count; i++)
        {
            if (prefabPlayers[team][i] == lastFilledSlot.GetComponent<CharacterInfo>().characterPrefab)
            {
                prefabPlayers[team].RemoveAt(i);
            }
        }


        //ask the slot to empty itself and restore the character gameObject
        lastFilledSlot.GetComponent<Slot>().removeSelf();
        lastChosenCharacter.SetActive(true);
        lastChosenCharacter.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Add(GameObject player)
    {
        if (!prefabPlayers.ContainsKey(team)){
            prefabPlayers.Add(team, new List<GameObject>());
        }
        prefabPlayers[team].Add(player);
        nbEquipedSlot = prefabPlayers[team].Count;
        Debug.Log(nbEquipedSlot);
        if (nbEquipedSlot == nbMax)
        {
            full = true;
            showButtonEnd();
        }
    }

    private void Awake()
    {
        instance = this;
        prefabPlayers = new Dictionary<int, List<GameObject>>();
    }

}
