using System.Collections;
using System.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; set; }

    public static int currentTick;
    private int currentTurn;
    public static int maxTick = 25;
    public int maxTurn = 5;

    public PlayerController actifCharacter;


    public GameObject TimeManagerText;
    public bool isPlaying;
    public GameObject prefabPlayer;
    public Stack<PlayerController> characterOrder;
    public GameObject EndTurnPanel;

    //Liste de piles d'appel de méthodes
    public List<Stack<Action>> turnTimeLine = new List<Stack<Action>>();

    void Awake()
    {
        instance = this;
        for (int i = 0; i < maxTick; i++)
        {
            turnTimeLine.Add(new Stack<Action>());
        }
        characterOrder = new Stack<PlayerController>();
        isPlaying = false;
        currentTick = 0;
        currentTurn = 0;
    }

    public void AddAction(Action calledMethod)
    {
        turnTimeLine[currentTick].Push(calledMethod);
    }

    public void AddNewCharacter(PlayerController new_character)
    {
        new_character.setId(currentTurn);
        actifCharacter = new_character;
        characterOrder.Push(new_character);
    }

    public IEnumerator PlayTick()
    {

        isPlaying = true;
        Stack<Action> currentStack = turnTimeLine[currentTick];
        float index = 0.0f;
        foreach (Action method in currentStack)
        {
            index++;
            method();
            yield return new WaitForSeconds(0.2f/index);
        }
        isPlaying = false;
        currentTick++;
        if (currentTick == maxTick)
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        actifCharacter.isControllable = false;
        if (currentTurn == maxTurn)
        {
            // End Game Method
            Debug.Log("Fin de la partie !");
        }
        else
        {
            ScoreManager.instance.ResetScore();
            EndTurnPanel.SetActive(true);
        }
    }

    public void BeginTurn()
    {
        EndTurnPanel.SetActive(false);
        currentTurn = currentTurn + 1;
        currentTick = 0;
        GameObject go = Instantiate(prefabPlayer);
        go.GetComponent<Character>().init(new Vector3(0,0,0), 100, 50, true);
        // ScoreManager.instance.SwitchTeam((currentTurn - 1) % 2);
        //NB : Je n'ai pas mis de PlayTick ici afin d'être sûr que la méthode ResetPosition a bien été ajouté au tick 0 avant de lancer le tick
    }

    public void ResetTurn()
    {
        currentTick = 0;
        currentTurn++;
    }


    // Update is called once per frame
    void Update()
    {
        string message = currentTurn.ToString() + '/' + currentTick.ToString();
        TimeManagerText.GetComponent<TMPro.TextMeshProUGUI>().text = message;

    }
}
