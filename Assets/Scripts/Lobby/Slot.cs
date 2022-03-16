using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    bool isEmpty = true;

    void placeCharacter(PointerEventData eventData)
    {
        isEmpty = false;
        eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;
        gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        //temporaire
        gameObject.GetComponent<CharacterInfo>().atk = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().atk;
        gameObject.GetComponent<CharacterInfo>().hp = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().hp;
        gameObject.GetComponent<CharacterInfo>().characterPrefab = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().characterPrefab;
        gameObject.GetComponent<CharacterInfo>().nameCharacter = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().nameCharacter;
        gameObject.GetComponent<CharacterInfo>().ability1 = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability1;
        gameObject.GetComponent<CharacterInfo>().ability2 = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability2;

        LobbyManager.instance.Add(gameObject.GetComponent<CharacterInfo>().characterPrefab);

        Destroy(eventData.pointerDrag.GetComponent<RectTransform>().gameObject);
    }

    void replaceCharacter(PointerEventData eventData)
    {

        eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;
        Sprite tmpSprite = gameObject.GetComponent<Image>().sprite;
        gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        CharacterInfo tmpInfo = gameObject.GetComponent<CharacterInfo>();
        int tmpAtk = tmpInfo.atk;
        int tmpHp = tmpInfo.hp;
        GameObject tmpPrefab = tmpInfo.characterPrefab;
        string tmpName = tmpInfo.nameCharacter;
        Ability tmpAbility1 = tmpInfo.ability1;
        Ability tmpAbility2 = tmpInfo.ability2;


        //temporaire
        gameObject.GetComponent<CharacterInfo>().atk = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().atk;
        gameObject.GetComponent<CharacterInfo>().hp = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().hp;
        gameObject.GetComponent<CharacterInfo>().characterPrefab = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().characterPrefab;
        gameObject.GetComponent<CharacterInfo>().nameCharacter = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().nameCharacter;
        gameObject.GetComponent<CharacterInfo>().ability1 = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability1;
        gameObject.GetComponent<CharacterInfo>().ability2 = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability2;


        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().atk = tmpAtk;
        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().hp = tmpHp;
        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().characterPrefab = tmpPrefab;
        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().nameCharacter = tmpName;
        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability1 = tmpAbility1;
        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().ability2 = tmpAbility2;



        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<Image>().sprite = tmpSprite;
        LobbyManager.instance.Add(gameObject.GetComponent<CharacterInfo>().characterPrefab);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(isEmpty)
            {
                placeCharacter(eventData);
            } else
            {
                replaceCharacter(eventData);
            }
        }
    }
}
