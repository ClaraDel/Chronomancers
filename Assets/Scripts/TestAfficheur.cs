using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAfficheur : MonoBehaviour
{

    public int porteeMin = 2;
    public int porteeMax = 3;
    public Vector3 positionOrigin = new Vector3(3,3,0);
    public List<Vector3> zoneEffet = new List<Vector3>();
    Afficheur a;



    // Start is called before the first frame update
    void Start()
    {
        a = Afficheur.create(positionOrigin, porteeMin, porteeMax, zoneEffet);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(a.cursor.hasValidated);
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (a.isDisplaying)
            {
                a.endDisplay();

            }
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            if (!a.isDisplaying)
            {
                a.display();

            }
        }


    }
}
