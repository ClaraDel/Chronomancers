using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomCharacterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject UIPerso;
    [SerializeField] GameObject avatar;
    public GameObject ability;
    CharacterInfo characterInfo;
    private Color highlightedColor;
    private Color normalColor;
    public bool pressed = false;

    public virtual void updateInfoUI()
    {
        UIPerso.transform.Find("Name").GetComponent<TMP_Text>().text = characterInfo.nameCharacter;
        UIPerso.transform.Find("Attributs").Find("Atk").GetComponent<TMP_Text>().text = "Atk : " + characterInfo.atk.ToString();
        UIPerso.transform.Find("Attributs").Find("Hp").GetComponent<TMP_Text>().text = "hp : " + characterInfo.hp.ToString();

        for (int i = 1; i <= characterInfo.abilities.Length; i++)
        {
            UIPerso.transform.Find("Skills").Find("Skill" + i).GetComponent<SkillSelection>().ability = characterInfo.abilities[i-1];
            UIPerso.transform.Find("Skills").Find("Skill" + i).GetComponent<Image>().sprite = characterInfo.abilities[i-1].image;
        }
    }


    public virtual void highlightButton()
    {
        gameObject.GetComponent<Image>().color = highlightedColor;
    }

    public virtual void resetButton()
    {
        gameObject.GetComponent<Image>().color = normalColor;
    }

    public virtual void displayUI()
    {
        if (avatar != null && UIPerso != null)
        {
            //displayUI();
            avatar.GetComponent<Image>().sprite = characterInfo.characterPrefab.GetComponent<SpriteRenderer>().sprite;

            UIPerso.SetActive(true);
            avatar.SetActive(true);
            ability.SetActive(false);
            avatar.GetComponent<Animator>().runtimeAnimatorController = characterInfo.characterPrefab.GetComponent<Animator>().runtimeAnimatorController;
        }

    }

    public virtual void hideUI()
    {
        if (UIPerso != null && avatar != null)
        {
            UIPerso.SetActive(false);
            avatar.SetActive(false);
            ability.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectionManager.mouseOnObject = true;
        if (SelectionManager.selected != null && SelectionManager.selected != gameObject)
        {
            SelectionManager.updateStatePreviousButton(normalColor);
        }
        highlightButton();

        //display UI if they are not null
        displayUI();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        SelectionManager.mouseOnObject = false;
        if (!pressed)
        {
            hideUI();
            resetButton();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pressed = true;
        displayUI();
        SelectionManager.selected = gameObject;
    }



    // Start is called before the first frame update
    void Start()
    {
        characterInfo = gameObject.GetComponent<CharacterInfo>();
        normalColor = new Color(0.7924528f, 0.6479174f, 0.6479174f);
        highlightedColor = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
