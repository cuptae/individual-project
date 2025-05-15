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
        mercenaryPortraits[myMercenaries.IndexOf(mercenary)].SetMercenary(mercenary);

        MercenaryPortraitUpdate();
    }

    public void SpawnMercenary(Mercenary mercenary, OverlayTile tile)
    {
        if (myMercenaries.Count >= maxMercenaryCount)
        {
            Debug.Log("병사 수가 최대치를 초과했습니다.");
            return;
        }
        Mercenary preMercenary;
        if(curMercenary != null)
        {
            preMercenary = curMercenary;
            preMercenary.HideMoveRange();
        }
        curMercenary = Instantiate(mercenary,tile.transform.position,Quaternion.identity);
        curMercenary.currentTile = tile.GetComponent<OverlayTile>();
        curMercenary.currentTile.gridLocation = tile.GetComponent<OverlayTile>().gridLocation;
        curMercenary.currentTile.CheckIsOnObject();
        curMercenary.ShowMoveRange();
        AddMercenary(curMercenary);
        Debug.Log(myMercenaries.Count);
    }
    public void ChangeMercenary(Mercenary mercenary)
    {
        if (myMercenaries.Contains(mercenary))
        {
            curMercenary.HideMoveRange();
            curMercenary = mercenary;
            curMercenary.ShowMoveRange();
            MercenaryPortraitUpdate();
        }
        else
        {
            Debug.Log("병사가 목록에 없습니다.");
        }
    }

    public void MercenaryPortraitUpdate()
    {
        foreach (var portrait in mercenaryPortraits)
        {
            portrait.UpdateFrameColor();
        }
    }
}
