using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MercenaryMovement : MonoBehaviour
{
    Tilemap tilemap;
    public int moveRange;
    public int attackRange;
    public int skillRange;

    public GameObject movableTile;


    void Awake()
    {
        tilemap = TilemapManager.Instance.tilemap;
    }

    void Start()
    {
        SetMovableTile();
    }


    /// <summary>
    /// 이동 가능 범위를 설정 하는 메서드
    /// </summary>
    public void SetMovableTile()
    {
        Vector3Int originCell = tilemap.WorldToCell(transform.position); // 현재 위치 기준
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
                    if (tilemap.HasTile(cellPos))
                    {
                        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
                        GameObject tile = Instantiate(movableTile, worldPos-TilemapManager.Instance.offset, Quaternion.identity);
                        tile.transform.SetParent(this.transform); // 혹은 다른 관리 오브젝트 밑에
                    }
                }
            }
        }
    }
}
