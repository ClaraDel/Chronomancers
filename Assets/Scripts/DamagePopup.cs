using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private float DISAPPEAR_TIME_MAX = 1.0f;
    private Vector3 moveVector;

    public static DamagePopup create(int damageAmount, GameObject character) {
        Debug.Log(character.transform.position);
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, character.transform.position + new Vector3(0.5f,0,0), Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.setDamageAmount(damageAmount);
        return damagePopup;
    }

    public void setDamageAmount(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIME_MAX;
        moveVector = new Vector3(.7f, 1) * 60f;
    }


    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = new Vector3(.7f, 1)*1;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * .8f * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        float disappearSpeed = 3f;

        if(disappearTimer > DISAPPEAR_TIME_MAX * 0.5f)
        {
            float increaseScale = 1.0f;
            transform.localScale += Vector3.one * increaseScale * Time.deltaTime;

        } else
        {
            float decreaseScale = 1.0f;
            transform.localScale -= Vector3.one * decreaseScale * Time.deltaTime;

        }
        if(disappearTimer < 0)
        {
            textColor.a -= disappearSpeed;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }

    }
}
