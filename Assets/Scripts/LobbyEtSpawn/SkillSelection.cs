using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    private Button button;
    public Ability ability;
    public GameObject abilityObject;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        abilityObject.GetComponent<AbilitySet>().ability = ability;
        abilityObject.GetComponent<AbilitySet>().setValues();

        abilityObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
