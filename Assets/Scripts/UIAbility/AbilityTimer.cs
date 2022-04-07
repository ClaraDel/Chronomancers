using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTimer : MonoBehaviour
{
    List<GameObject> abilities;
    public static AbilityTimer instance;
    public void updateUIAbility()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            GameObject countDown_ability_i = abilities[i].transform.Find("CountDown").gameObject;
            Timer timer = countDown_ability_i.GetComponent<Timer>();
            timer.playTick();
        }
    }

    public void resetTimers()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            abilities[i].transform.Find("CountDown").GetComponent<Timer>().resetTimer();
        }
    }

    public void launchUIAbility(int i)
    {
        abilities[i].SetActive(true);
        GameObject countDown_ability_i = abilities[i].transform.Find("CountDown").gameObject;
        countDown_ability_i.SetActive(true);
        Debug.Log(countDown_ability_i.activeSelf);
        Timer timer = countDown_ability_i.GetComponent<Timer>();
        timer.startCountDown();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        abilities = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            abilities.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
