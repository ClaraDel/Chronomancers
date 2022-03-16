using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject UIPerso;
    [SerializeField] GameObject avatar;
    [SerializeField] GameObject ability;
    CharacterInfo characterInfo;
    private Color highlightedColor;
    private Color normalColor;
    public bool pressed = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SelectionManager.selected != null && SelectionManager.selected != gameObject)
        {
            SelectionManager.selected.GetComponent<SelectController>().pressed = false;
            SelectionManager.selected.GetComponent<Image>().color = normalColor;
            SelectionManager.selected = null;
        }

        gameObject.GetComponent<Image>().color = highlightedColor;

        UIPerso.transform.Find("Name").GetComponent<TMP_Text>().text = characterInfo.nameCharacter;
        UIPerso.transform.Find("Attributs").Find("Atk").GetComponent<TMP_Text>().text = "Atk : " + characterInfo.atk.ToString();
        UIPerso.transform.Find("Attributs").Find("Hp").GetComponent<TMP_Text>().text = "hp : " + characterInfo.hp.ToString();

        //transformer en for loop si il y a + ou - de 2 skills
        UIPerso.transform.Find("Skills").Find("Skill1").GetComponent<SkillSelection>().ability = characterInfo.ability1;
        UIPerso.transform.Find("Skills").Find("Skill1").GetComponent<Image>().sprite = characterInfo.ability1.image;
        UIPerso.transform.Find("Skills").Find("Skill2").GetComponent<SkillSelection>().ability = characterInfo.ability2;
        UIPerso.transform.Find("Skills").Find("Skill2").GetComponent<Image>().sprite = characterInfo.ability2.image;

        avatar.GetComponent<Image>().sprite = characterInfo.characterPrefab.GetComponent<SpriteRenderer>().sprite;

        UIPerso.SetActive(true);
        avatar.SetActive(true);
        ability.SetActive(false);


    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (!pressed)
        {
            UIPerso.SetActive(false);
            avatar.SetActive(false);
            ability.SetActive(false);
            gameObject.GetComponent<Image>().color = normalColor;

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pressed = true;
        UIPerso.SetActive(true);
        avatar.SetActive(true);
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
