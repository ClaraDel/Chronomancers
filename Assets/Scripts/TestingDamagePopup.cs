using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamagePopup : MonoBehaviour
{
    private Character c1;

    void Start()
    {
       //DamagePopup.create(Vector3.zero, 300);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 pos = new Vector3(worldPosition.x, worldPosition.y, 0);
            //DamagePopup.create(300, player);
            Character c = Character.create(pos, 100, 50);
            if(c1 == null)
            {
                c1 = c;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("attack");
            if (c1 != null)
            {
               // c1.atk.applyAttack(c1.gameObject, c1.gameObject.transform.position + new Vector3(20,0,0));
            }

        }

    }

}
