using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAbilityButton : CustomCharacterButton
{
    public override void displayUI()
    {
        ability.GetComponent<AbilitySet>().ability = gameObject.GetComponent<AbilitySet>().ability;
        ability.GetComponent<AbilitySet>().setValues();
        ability.SetActive(true);
    }

    public override void hideUI()
    {
        ability.SetActive(false);
    }

    public override void highlightButton()
    {
        Color c = gameObject.transform.Find("Color").GetComponent<Image>().color;
        c.a = 0;
        gameObject.transform.Find("Color").GetComponent<Image>().color = c;
    }

    public override void resetButton()
    {
        Color c = gameObject.transform.Find("Color").GetComponent<Image>().color;
        c.a = 0.4f;
        gameObject.transform.Find("Color").GetComponent<Image>().color = c;
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
