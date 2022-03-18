using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReceiver : Receiver
{

    public override void spread(List<Info> infos)
    {
        base.spread(infos);
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<CharacterInfo>().swapInfoWith(infos[i]);
            gameObject.transform.GetChild(i).name = gameObject.transform.GetChild(i).GetComponent<CharacterInfo>().nameCharacter;
        }
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
