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

        gameObject.GetComponent<Info>().addInfo(eventData.pointerDrag.GetComponent<Info>());

        SlotsManager.instance.Add(gameObject.GetComponent<Info>());
        SlotsManager.instance.Save(gameObject, eventData.pointerDrag);

        eventData.pointerDrag.GetComponent<RectTransform>().gameObject.SetActive(false);

    }

    void replaceCharacter(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;

        Info Info = gameObject.GetComponent<Info>();
        gameObject.GetComponent<Info>().swapInfoWith(eventData.pointerDrag.GetComponent<Info>());
        
        SlotsManager.instance.Replace(eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<Info>(),
            gameObject.GetComponent<Info>());
        SlotsManager.instance.Save(gameObject, eventData.pointerDrag);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(isEmpty && !SlotsManager.instance.isFull())
            {
                placeCharacter(eventData);
            } else if (SlotsManager.instance.isFull())
            {
                SlotsManager.instance.RemoveObjectFromLastSlot();
                placeCharacter(eventData);
            }
            else
            {
                replaceCharacter(eventData);
            }
        }
    }
}
