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

    /* void Being(int second)
     {
         remainingDuration = second;
         playTick();
     }*/

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


    }


    public void playTick(int coolDown)
    {
        if (isCounting)
        {
            remainingDuration = coolDown;
            if (remainingDuration <= 0)
            {
                onEnd();
            }
            UIText.text = remainingDuration.ToString();
            UIFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
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

        isCounting = false;
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
