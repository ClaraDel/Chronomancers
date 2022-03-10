using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAfficheur : MonoBehaviour
{
    Afficheur a;
    public List<Vector3> positions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            a = Afficheur.create(new Vector3(0, 0, 0), positions);
            a.display();
        }

    }
}
