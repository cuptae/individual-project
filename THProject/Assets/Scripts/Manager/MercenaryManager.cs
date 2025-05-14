using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryManager : MonoSingleton<MercenaryManager>
{
    int maxMercenaryCount = 4; // 최대 병사 수
    public int curMercenaryCount = 0; // 현재 병사 수
    public List<Mercenary> myMercenaries = new List<Mercenary>();
    public List<MercenaryPortrait> mercenaryPortraits = new List<MercenaryPortrait>();
    public Mercenary curMercenary; // 현재 선택된 병사

    public void AddMercenary(Mercenary mercenary)
    {
        if (curMercenaryCount > maxMercenaryCount)
        {
            Debug.Log("병사 수가 최대치를 초과했습니다.");
            return;
        }
        myMercenaries.Add(mercenary);
        mercenaryPortraits[myMercenaries.IndexOf(mercenary)].SetPortrait(mercenary.dataSO.mercenaryPortrait);
        curMercenaryCount++;
    }

    public void ChangeMercenary(Mercenary mercenary)
    {
        if (myMercenaries.Contains(mercenary))
        {
            curMercenary = mercenary;
        }
        else
        {
            Debug.Log("병사가 목록에 없습니다.");
        }
    }

    public void SpawnMercenary(Mercenary mercenary, OverlayTile tile)
    {
        if (curMercenaryCount > maxMercenaryCount)
        {
            Debug.Log("병사 수가 최대치를 초과했습니다.");
            return;
        }

    }
}
