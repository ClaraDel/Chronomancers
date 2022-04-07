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


    public void playTick()
    {
        if(isCounting)
        {
            UIText.text = remainingDuration.ToString();
            Debug.Log(duration);
            Debug.Log(remainingDuration);
            UIFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            if (remainingDuration == -1)
            {
                onEnd();
            }
        }
    }

    public void startCountDown()
    {
        Debug.Log("countdown");
        isCounting = true;
        gameObject.SetActive(true);
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
        this.duration = int.Parse(UIText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
