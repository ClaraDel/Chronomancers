using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySet : MonoBehaviour
{
    public Ability ability;

    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI description;
    public Image image;
    public TextMeshProUGUI canalisation;
    public TextMeshProUGUI limite;

    public int canalisationActuelle;
    public int limiteActuelle;
    // Start is called before the first frame update
    void Start()
    {
        setValues();
    }

    public void setValues()
    {
        abilityName.text = ability.abilityName;

        description.text = ability.description;

        image.sprite = ability.image;

        if (ability.canalisation != -1) canalisation.text = ability.canalisation.ToString();
        else canalisation.text = "PSV";
        canalisationActuelle = ability.canalisation;

        if (ability.limite != -1) limite.text = ability.limite.ToString();
        else limite.text = "PSV";
        limiteActuelle = ability.limite;
    }
}
