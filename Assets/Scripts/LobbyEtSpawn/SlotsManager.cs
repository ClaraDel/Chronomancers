using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{

    public int nbEquipedSlot = 0;
    public int team = 1;
    public bool full = false;


    [SerializeField] int nbMax = 5;
    [SerializeField] GameObject[] lobbies;
    

    public Dictionary<int, List<Info>> infos;

    public GameObject [] confirmButtons;
    private GameObject lastFilledSlot;
    private GameObject lastChosenCharacter;

    [SerializeField] GameObject [] targets;
    public List <Receiver> receivers;

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
        full = false;
        EndCharacterSelection();
    }

    public void EndCharacterSelection()
    {
        if(receivers.Count == 1)
            receivers[0].receive(infos[team]);
        else
            receivers[team - 1].receive(infos[team]);

        removeUnEmptySlots();
        infos[team] = new List<Info>();

        team += 1;
        if(team > 2)
        {
            team = 1;
        }
    }

    public void removeUnEmptySlots()
    {
        if(nbMax == 1)
        {
            lastFilledSlot.SetActive(false);
            lastFilledSlot.transform.parent.gameObject.SetActive(false);
            nbEquipedSlot = 0;
            full = false;
        } else
        {
            //not necessary here
        }
    }

    public void confirmSelectionTeam2()
    {
        lobbies[1].SetActive(false);
        targets[0].SetActive(true);
        //characterInfoPanel.SetActive(true);
        EndCharacterSelection();
        gameObject.SetActive(false);
    }

    public void Replace(Info oldInfo, Info newInfo)
    {
        for(int i = 0; i < infos[team].Count; i++)
        {
            if(infos[team][i] == oldInfo)
            {
                infos[team][i] = newInfo;
            }
        }
    }

    public void RemoveObjectFromLastSlot()
    {
        //remove prefab from the list of prefabs
        for (int i = 0; i < infos[team].Count; i++)
        {
            if (infos[team][i] == lastFilledSlot.GetComponent<CharacterInfo>())
            {
                infos[team].RemoveAt(i);
            }
        }


        //ask the slot to empty itself and restore the character gameObject
        lastFilledSlot.GetComponent<Slot>().removeSelf();
        lastChosenCharacter.SetActive(true);
        lastChosenCharacter.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Add(Info playerInfo)
    {
        if (!infos.ContainsKey(team)){
            infos.Add(team, new List<Info>());
        }
        infos[team].Add(playerInfo);

        nbEquipedSlot = infos[team].Count;
        if (nbEquipedSlot == nbMax)
        {
            full = true;
            showButtonEnd();
            nbEquipedSlot = 0;
        }
    }

    private void Awake()
    {
        infos = new Dictionary<int, List<Info>>();
        receivers = new List<Receiver>();
        if (targets != null)
        {
            for(int i = 0; i < targets.Length; i++)
            {
                receivers.Add(targets[i].GetComponentInChildren<Receiver>());
            }
        }
    }

    private void Start()
    {
        //print(GetInstanceID());
    }
}
