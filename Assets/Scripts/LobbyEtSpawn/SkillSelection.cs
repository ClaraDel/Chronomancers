using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    public Ability ability;
    public GameObject abilityObject;
    public bool pressed = false;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        pressed = true;
        abilityObject.GetComponent<AbilitySet>().ability = ability;
        abilityObject.GetComponent<AbilitySet>().setValues();

        abilityObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!pressed)
        {
            abilityObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1); ;
        abilityObject.GetComponent<AbilitySet>().ability = ability;
        abilityObject.GetComponent<AbilitySet>().setValues();

        abilityObject.SetActive(true);
    }
}
