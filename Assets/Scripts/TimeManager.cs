using System.Collections;
using System.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; set; }
    private int turnLength = 15;
    private int numberTurn;
    public int currentTick;

    public GameObject prefabPlayer;
    public Queue<PlayerController> characterOrder;
    public PlayerController actifCharacter;

    //Liste de piles d'appel de méthodes
    public List<Queue<Action>> turnTimeLine = new List<Queue<Action>>();

    void Awake()
    {
        instance = this;
        for (int i = 0; i < turnLength; i++)
        {
            turnTimeLine.Add(new Queue<Action>());
        }
        characterOrder = new Queue<PlayerController>();
        currentTick = 0;
        numberTurn = 0;
    }

    public void AddAction(Action calledMethod)
    {
        turnTimeLine[currentTick].Enqueue(calledMethod);
    }

    public void NewCharacter(PlayerController new_character)
    {
        actifCharacter = new_character;
        characterOrder.Enqueue(new_character);
    }

    public void playTick()
    {
        Queue<Action> currentQueue = turnTimeLine[currentTick];
        foreach (Action method in currentQueue)
        {
            method();
            Debug.Log(currentTick);
        }
        currentTick = (currentTick + 1) % turnLength;
        if (currentTick == 0)
        {
            //endTurn Method ??
            numberTurn = numberTurn + 1;
            actifCharacter.isControllable = false;
            Instantiate(prefabPlayer, new Vector3(6,0,0), prefabPlayer.transform.rotation);
            playTick();
        }
    }

    public void resetTurn()
    {
        currentTick = 0;
        numberTurn++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string message = numberTurn.ToString() + '/' + currentTick.ToString();
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = message;
    }
}
