using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamagePopup : MonoBehaviour
{
    [SerializeField] private GameObject player;

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
            DamagePopup.create(300, player);
        }

    }

}
