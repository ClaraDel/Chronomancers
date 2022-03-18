using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    bool isEmpty = true;


    public void removeSelf()
    {
        //delete sprite
        gameObject.GetComponent<Image>().sprite = null;
        Color tmpColor = gameObject.GetComponent<Image>().color;
        tmpColor.a = 0;
        gameObject.GetComponent<Image>().color = tmpColor;
    }

    void placeCharacter(PointerEventData eventData)
    {
        isEmpty = false;

        eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;

        gameObject.GetComponent<CharacterInfo>().addInfo(eventData.pointerDrag.GetComponent<CharacterInfo>());

        LobbyManager.instance.Add(gameObject.GetComponent<CharacterInfo>().characterPrefab);
        LobbyManager.instance.Save(gameObject, eventData.pointerDrag);

        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.SetActive(false);

    }

    void replaceCharacter(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;

        CharacterInfo Info = gameObject.GetComponent<CharacterInfo>();
        gameObject.GetComponent<CharacterInfo>().swapInfoWith(eventData.pointerDrag.GetComponent<CharacterInfo>());
        
        LobbyManager.instance.Replace(eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<CharacterInfo>().characterPrefab,
            gameObject.GetComponent<CharacterInfo>().characterPrefab);
        LobbyManager.instance.Save(gameObject, eventData.pointerDrag);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(isEmpty && !LobbyManager.instance.isFull())
            {
                placeCharacter(eventData);
            } else if (LobbyManager.instance.isFull())
            {
                LobbyManager.instance.RemoveObjectFromLastSlot();
                placeCharacter(eventData);
            }
            else
            {
                replaceCharacter(eventData);
            }
        }
    }
}
