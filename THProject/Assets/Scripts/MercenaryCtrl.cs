using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public class MercenaryCtrl : MonoBehaviour
{
    public int moveRange;
    public int attackRange;
    public int skillRange;

    public GameObject movableTile;

    void Start()
    {
        SetMovableTile();
    }

    public void SetMovableTile()
    {
        Vector3Int originCell = TilemapManager.instance.tilemap.WorldToCell(transform.position); // 현재 위치 기준
        int range = moveRange;

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                // 맨해튼 거리 계산
                if (Mathf.Abs(x) + Mathf.Abs(y) <= range)
                {
                    Vector3Int cellPos = new Vector3Int(originCell.x + x, originCell.y + y, 0);

                    // 예: 해당 위치에 타일이 존재하고, 이동 가능한 경우만 표시
                    if (TilemapManager.instance.tilemap.HasTile(cellPos))
                    {
                        Vector3 worldPos = TilemapManager.instance.tilemap.GetCellCenterWorld(cellPos);
                        GameObject tile = Instantiate(movableTile, worldPos-TilemapManager.instance.offset, Quaternion.identity);
                        tile.transform.SetParent(this.transform); // 혹은 다른 관리 오브젝트 밑에
                    }
                }
            }
        }
    }
}
