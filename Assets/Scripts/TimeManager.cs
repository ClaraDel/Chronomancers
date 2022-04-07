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
    public static int maxTick = 10;
    public static int maxTurn = 4;

    [SerializeField] SlotsManager slotsManager;


    public PlayerController actifCharacter;

    public GameObject TimeManagerText;
    public bool isPlaying;
    public GameObject prefabPlayer;
    public Stack<PlayerController> characterOrder;
    public GameObject EndTurnPanel;
    public GameObject [] spawnZones;
    [SerializeField] private Fade fade;
    [SerializeField] GameObject characterInfoPanel;
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
        TimeManagerText = GameObject.Find("TimeManagerText");
        characterInfoPanel.SetActive(false);
    }

    public void AddAction(Action calledMethod)
    {
        turnTimeLine[currentTick].Push(calledMethod);
    }

    public void AddNewCharacter(PlayerController new_character)
    {
        actifCharacter = new_character;
        characterOrder.Push(new_character);
    }

    public IEnumerator PlayTick()
    {
        AbilityTimer.instance.updateUIAbility();
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
        AbilityTimer.instance.resetTimers();
        actifCharacter.isControllable = false;
        if (currentTurn == maxTurn)
        {
            // End Game Method
            ScoreManager.instance.UpdateScoreTotal();
            Debug.Log("Fin de la partie !");
            fade.fadeIn();
        }
        else
        {
            ScoreManager.instance.UpdateScoreTotal();
            ScoreManager.instance.ResetScore();
            //EndTurnPanel.SetActive(true);
            spawnZones[(currentTurn % 2)].SetActive(true);

        }
    }

    public void BeginTurn()
    {
        EndTurnPanel.SetActive(false);
        slotsManager.EndCharacterSelection();
        characterInfoPanel.SetActive(true);
        spawnZones[(currentTurn % 2)].SetActive(false);
        currentTurn = currentTurn + 1;
        currentTick = 0;
        ScoreManager.instance.SwitchTeam((currentTurn - 1) % 2);
        GameObject go = Instantiate(prefabPlayer);
        switch (go.GetComponent<Character>().getType())
        {
            case Character.type.roublard:
                go.GetComponent<Roublard>().init(true);
                break;
            case Character.type.barbare:
                go.GetComponent<Barbare>().init(true);
                break;
            case Character.type.paladin:
                go.GetComponent<Paladin>().init(true);
                break;
            case Character.type.pyromancien:
                go.GetComponent<Pyromancien>().init(true);
                break;
            case Character.type.ranger:
                go.GetComponent<Ranger>().init(true);
                break;
            default:
                break;
        }
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
