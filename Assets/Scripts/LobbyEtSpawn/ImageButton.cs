using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectionManager.mouseOnObject = true;
        gameObject.GetComponent<Image>().color = new Color(1,1,1);
         
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SelectionManager.mouseOnObject = false;
        gameObject.GetComponent<Image>().color = new Color(0.7924528f, 0.6479174f, 0.6479174f);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(0.7924528f, 0.6479174f, 0.6479174f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
