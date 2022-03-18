using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : Info
{
    public GameObject characterPrefab;
    public int atk;
    public int hp;
    public string nameCharacter;
    public Ability [] abilities;

    public override void swapInfoWith(Info otherInfo)
    {
        CharacterInfo otherCharacterInfo = (CharacterInfo)otherInfo;

        //save otherInfo
        GameObject tmpPrefab = otherCharacterInfo.characterPrefab;
        string tmpName = otherCharacterInfo.nameCharacter;
        int tmpAtk = otherCharacterInfo.atk;
        int tmpHp = otherCharacterInfo.hp;
        Ability [] tmpAbilities = otherCharacterInfo.abilities;
        Sprite tmpSprite = otherCharacterInfo.gameObject.GetComponent<Image>().sprite;
        Color tmpColor = otherCharacterInfo.gameObject.GetComponent<Image>().color;

        otherInfo.addInfo(this);

        this.addCharacterInfo(tmpName, tmpPrefab, tmpHp, tmpAtk, tmpAbilities,tmpSprite,tmpColor);
    }

    public  void addCharacterInfo(string nameCharacter,GameObject characterPrefab, int atk, int hp, Ability [] abilities, Sprite sprite, Color color)
    {
        this.nameCharacter = nameCharacter;
        this.atk = atk;
        this.hp = hp;
        this.characterPrefab = characterPrefab;
        this.abilities = abilities;
        gameObject.GetComponent<Image>().sprite = sprite;
        gameObject.GetComponent<Image>().color = color;
    }

    public override void addInfo(Info info)
    {
        CharacterInfo characterInfo = (CharacterInfo)info;
        addCharacterInfo(characterInfo.nameCharacter, characterInfo.characterPrefab, 
            characterInfo.atk, characterInfo.hp, characterInfo.abilities, 
            characterInfo.gameObject.GetComponent<Image>().sprite, characterInfo.gameObject.GetComponent<Image>().color);
    }

    private void Start()
    {
       
    }

}
