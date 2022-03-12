using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTilePopup : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public float freq = 1;
    bool decrease = true;
   
    public static RedTilePopup create(Vector3 position)
    {
        //change pfDamage later
        Transform redTilePopupTransform = Instantiate(GameAssets.i.pfRedTilePopup, position, Quaternion.identity);
        RedTilePopup redTilePopup = redTilePopupTransform.GetComponent<RedTilePopup>();
        return redTilePopup;
    }

    public void destroy()
    {
        Destroy(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject square = gameObject.transform.Find("Square").gameObject;
        spriteRenderer = square.transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = spriteRenderer.color;
        if(decrease)
        {
            tmp.a -= Time.deltaTime * freq;
            if(tmp.a <= 0.5)
            {
                decrease = false;
            }
        } else
        {
            tmp.a += Time.deltaTime * freq;
            if(tmp.a >= 1)
            {
                decrease = true;
            }
        }
        spriteRenderer.color = tmp;
    }
}
