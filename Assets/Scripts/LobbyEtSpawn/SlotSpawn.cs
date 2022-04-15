using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotSpawn : Slot
{
    [SerializeField] GameObject UIAbility;

    public virtual void displayUI()
    {
        Debug.Log("Display");
        CharacterInfo characterInfo = (CharacterInfo) gameObject.GetComponent<Info>();
        UIAbility.SetActive(true);

        for (int i = 1; i <= characterInfo.abilities.Length; i++)
        {
            UIAbility.transform.Find("Ability" + i).GetComponent<AbilitySet>().ability = characterInfo.abilities[i - 1];
            UIAbility.transform.Find("Ability" + i).GetComponent<AbilitySet>().setValues();
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        displayUI();
        AbilityTimer.instance.setAbilities();

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
