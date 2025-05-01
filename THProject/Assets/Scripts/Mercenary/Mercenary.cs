using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mercenary : MonoBehaviour
{
    public OverlayTile currentTile; // 현재 타일
    public int moveRange = 3; // 이동 범위
    MouseController mouseController; // 마우스 컨트롤러
    public bool isSelected = false; // 선택 여부
    void Awake()
    {
        mouseController = GameObject.FindWithTag("MouseCtrl").GetComponent<MouseController>();
    }

    public void ShowMoveRange()
    {
        Vector2Int origin = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y);

        foreach (var tilePair in TilemapManager.Instance.map)
        {
            Vector2Int tilePos = tilePair.Key;
            GameObject tileObj = tilePair.Value;
            OverlayTile overlayTile = tileObj.GetComponent<OverlayTile>();

            int distance = Mathf.Abs(tilePos.x - origin.x) + Mathf.Abs(tilePos.y - origin.y);
            if (distance <= moveRange)
            {
                overlayTile.ShowTile();
            }
        }
    }

    public void MoveToTile(OverlayTile targetTile)
    {
        if(mouseController.curMode != MouseMode.MercenaryMove) return;
        if (!targetTile.isOnMoveRange) return;

        var path = new Pathfinding().FindPath(currentTile, targetTile);

        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
    }

    IEnumerator FollowPath(List<OverlayTile> path)
    {
        foreach (var tile in path)
        {
            transform.position = tile.transform.position + new Vector3(0, 0.25f, 0);
            yield return new WaitForSeconds(0.1f);
        }

        currentTile.isOnObject = false;
        currentTile = path.Last();
        currentTile.isOnObject = true;
    }
}
