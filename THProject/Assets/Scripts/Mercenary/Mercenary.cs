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
    Pathfinding pathfinding; // 경로 탐색기
    public bool isMoving = false; // 이동 중 여부
    void Awake()
    {
        mouseController = GameObject.FindWithTag("MouseCtrl").GetComponent<MouseController>();
        pathfinding = new Pathfinding();
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
        if (isMoving) return; // 이미 이동 중이면 무시
        if(mouseController.curMode != MouseMode.MercenaryMove) return;
        if (!targetTile.isOnMoveRange) return;

        var path = pathfinding.FindPath(currentTile, targetTile);

        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
    }

    IEnumerator FollowPath(List<OverlayTile> path)
    {
        isMoving = true;

        foreach (var tile in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = tile.transform.position + new Vector3(0, 0.25f, 0);
            float elapsedTime = 0f;
            float duration = 0.2f; // 이동 시간 (조절 가능)

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // 다음 프레임까지 대기
            }

            transform.position = endPos; // 보정
        }

        isMoving = false;
        currentTile.isOnObject = false;
        currentTile = path.Last();
        currentTile.isOnObject = true;
    }
}
