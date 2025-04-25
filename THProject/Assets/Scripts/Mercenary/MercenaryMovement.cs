using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MercenaryMovement : MonoBehaviour
{
    Tilemap tilemap; // 타일맵
    public int moveRange = 3; // 이동 범위
    public GameObject onOverlayTile; // 용병이 서있는 타일
    void Awake()
    {
        tilemap = GameObject.FindWithTag("Map").GetComponent<Tilemap>(); // 타일맵 컴포넌트 가져오기
    }

    void Start()
    {
        SetMovableTile();
    }


    /// <summary>
    /// 이동 가능 범위를 나타내는 메서드
    /// </summary>
    public void SetMovableTile()
    {
        Vector3Int originCell = tilemap.WorldToCell(transform.position); // 현재 위치 기준
        Vector2Int originCell2D = new Vector2Int(originCell.x, originCell.y);
        int range = moveRange;

        foreach (var tile in TilemapManager.Instance.map)
        {
            Vector2Int cellPos = tile.Key;
            GameObject overlayTile = tile.Value;

            // 맨해튼 거리 계산
            if (Mathf.Abs(cellPos.x - originCell2D.x) + Mathf.Abs(cellPos.y - originCell2D.y) <= range)
            {
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f); // 이동 가능한 타일 반투명 처리
            }
            else
            {
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // 이동 불가능한 타일 투명 처리
            }
        }
    }
}
