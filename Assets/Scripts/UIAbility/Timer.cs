using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text UIText;
    [SerializeField] Image UIFill;
    public int duration;
    public int remainingDuration;
    public bool isCounting = false;

    void Being(int second)
    {
        remainingDuration = second;
        playTick();
    }

    public void resetTimer()
    {
        onEnd();
    }

    public void setCountTimer(int count)
    {
       
        UIText.text = count.ToString();
        this.duration = int.Parse(UIText.text);
        this.remainingDuration = duration;
        UIFill.fillAmount = 1.0f;
        Debug.Log("set count duration " + gameObject.transform.parent.name + " " + duration);
        Debug.Log("set count remaining " + gameObject.transform.parent.name + " " + remainingDuration);

    }


    public void playTick()
    {
        Debug.Log("Play Tick duration "+ gameObject.transform.parent.name+ " " + duration);
        Debug.Log("Play Tick remaining " + gameObject.transform.parent.name + " " + remainingDuration);
        if (isCounting)
        {
            UIText.text = remainingDuration.ToString();
            Debug.Log(duration);
            Debug.Log(remainingDuration);
            UIFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration-=2;
            if (remainingDuration < -1)
            {
                onEnd();
            }
        }
    }

    public void startCountDown()
    {
        isCounting = true;
        gameObject.SetActive(true);
        remainingDuration = duration;
    }

    void onEnd()
    {
        Debug.Log("on end duration " + duration);
        Debug.Log("on end remaining " + remainingDuration); isCounting = false;
        gameObject.SetActive(false);

        UIText.text = duration.ToString();
        UIFill.fillAmount = 1.0f;
        remainingDuration = duration;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
