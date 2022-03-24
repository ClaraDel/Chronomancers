using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoPanel : MonoBehaviour
{
    public static CharacterInfoPanel instance;
    public CharacterInfoScriptable characterInfo;
    public AbilitySet ability0;
    public AbilitySet ability1;
    public AbilitySet ability2;
    public TextMeshProUGUI Nom;
    public Image image;
    public TextMeshProUGUI PV;
    public TextMeshProUGUI Attaque;
    public TextMeshProUGUI Description;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.GetComponent<CanvasGroup>().blocksRaycasts =  false ;

    }

    void setSelf(){
        gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha == 1)? 0 : 1;
        ability0.ability = characterInfo.ability0;
        ability1.ability = characterInfo.ability1;
        ability2.ability = characterInfo.ability2;
        ability0.setValues();
        ability1.setValues();
        ability2.setValues();
        Nom.text = characterInfo.characterName;
        image = characterInfo.profilPicture;
        PV.text = characterInfo.maxPV.ToString();
        Attaque.text = characterInfo.valueAttaque.ToString();
        Description.text = characterInfo.description;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            instance.GetComponent<CanvasGroup>().alpha = (instance.GetComponent<CanvasGroup>().alpha == 0.0f)? 1.0f : 0.0f;
            // this.setSelf();
        }
    }

    public bool getIfPaused(){
        return this.GetComponent<CanvasGroup>().alpha == 1.0f;
    }


}
