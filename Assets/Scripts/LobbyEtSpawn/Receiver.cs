using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{

    List<Info> infos;

    public void receive(List<Info> infos)
    {
        this.infos = infos;
        spread(infos);
    }

    // spread the info to children 
    public virtual void spread(List<Info> infos)
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
