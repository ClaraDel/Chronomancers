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

    //Liste de piles d'appel de méthode
    public List<Queue<Action>> turnTimeLine = new List<Queue<Action>>();

    void Awake()
    {
        instance = this;
        for (int i = 0; i < turnLength; i++)
        {
            turnTimeLine.Add(new Queue<Action>());
        }
        currentTick = 0;
        numberTurn = 0;
    }

    public void AddAction(Action calledMethod)
    {
        turnTimeLine[currentTick].Enqueue(calledMethod);
    }

    public void playTick()
    {
        Queue<Action> currentQueue = turnTimeLine[currentTick];
        foreach (Action method in currentQueue)
        {
            method();
        }
        currentTick = (currentTick + 1) % turnLength;
        numberTurn = (currentTick == 0) ? numberTurn + 1 : numberTurn;
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
