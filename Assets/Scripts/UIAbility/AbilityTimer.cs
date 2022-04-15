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

    public Timer getAbility(int i)
    {
        Debug.Log("getAbility " + abilities[i - 1].gameObject.name);
        return abilities[i - 1].transform.Find("CountDown").GetComponent<Timer>();
    }



    public void resetTimers()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            abilities[i].transform.Find("CountDown").GetComponent<Timer>().resetTimer();
        }
        gameObject.SetActive(false);
    }

    public void setAbilities()
    {
        abilities = new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            abilities.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void launchUIAbility(int i)
    {
        abilities[i - 1].SetActive(true);
        GameObject countDown_ability_i = abilities[i - 1].transform.Find("CountDown").gameObject;
        countDown_ability_i.SetActive(true);
        Timer timer = countDown_ability_i.GetComponent<Timer>();
        timer.startCountDown();
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
