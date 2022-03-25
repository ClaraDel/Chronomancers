using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerReceiver : Receiver
{
    public override void spread(List<Info> infos)
    {
        base.spread(infos);
        Info onlyInfo = infos[0];
        CharacterInfo info = (CharacterInfo)onlyInfo;
        spreadSpawnPos(info);
        TimeManager.instance.prefabPlayer = info.getPrefab();
    }

    public void spreadSpawnPos(CharacterInfo info)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(info.position);
        info.characterPrefab.GetComponent<MoveManager>().positionSpawnXTeam0 = Mathf.RoundToInt(info.position.x -0.5f);
        info.characterPrefab.GetComponent<MoveManager>().positionSpawnYTeam0 = Mathf.RoundToInt(info.position.y -0.5f);
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
