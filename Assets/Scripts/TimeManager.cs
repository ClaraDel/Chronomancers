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
    public static int maxTick = 15;
    private int maxTurn = 5;

    public bool isPlaying;
    public GameObject prefabPlayer;
    public Stack<PlayerController> characterOrder;
    public PlayerController actifCharacter;
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

    public void NewCharacter(PlayerController new_character)
    {
        actifCharacter = new_character;
        characterOrder.Push(new_character);
    }

    public void PlayTick()
    {
        isPlaying = true;
        Stack<Action> currentStack = turnTimeLine[currentTick];
        foreach (Action method in currentStack)
        {
            method.Invoke();
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
            EndTurnPanel.SetActive(true);
        }
    }

    public void BeginTurn()
    {
        EndTurnPanel.SetActive(false);
        currentTurn = currentTurn + 1;
        currentTick = 0;
        Instantiate(prefabPlayer, new Vector3(6, 0, 0), prefabPlayer.transform.rotation);
        //NB : Je n'ai pas mis de PlayTick ici afin d'être sûr que la méthode ResetPosition a bien été ajouté au tick 0 avant de lancer le tick
    }

    public void ResetTurn()
    {
        currentTick = 0;
        currentTurn++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string message = currentTurn.ToString() + '/' + currentTick.ToString();
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = message;
    }
}
