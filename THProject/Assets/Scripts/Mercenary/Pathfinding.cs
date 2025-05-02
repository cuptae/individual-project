using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A* 알고리즘을 통해 시작 타일에서 목표 타일까지 최적 경로를 찾는 클래스.
/// 대각선 이동도 허용됨.
/// </summary>
public class Pathfinding
{
    /// <summary>
    /// 주어진 시작 타일(startTile)과 목표 타일(endTile) 사이의 경로를 찾는다.
    /// </summary>
    public List<OverlayTile> FindPath(OverlayTile startTile, OverlayTile endTile)
    {
        List<OverlayTile> openList = new List<OverlayTile>(); // 탐색할 후보 타일들
        HashSet<OverlayTile> closedList = new HashSet<OverlayTile>(); // 이미 확인한 타일들

        openList.Add(startTile);
        startTile.G = 0; // 시작 타일의 실제 비용은 0
        startTile.H = GetManhattanDistance(startTile, endTile) * 10; // 휴리스틱 비용 계산
        startTile.previousTile = null;

        while (openList.Count > 0)
        {
            // F 값이 가장 낮은 타일을 선택
            // openList를 오름차순으로 정렬하여 가장 첫번째 값(가장 작은 F값)을 선택
            OverlayTile currentTile = openList.OrderBy(t => t.F).First();

            // 목표 지점에 도달했으면 경로 반환
            if (currentTile == endTile)
                return ReconstructPath(endTile);

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            // 현재 타일의 모든 이웃 타일 검사 (대각선 포함)
            foreach (var neighbor in GetNeighborTiles(currentTile))
            {
                if (closedList.Contains(neighbor) || neighbor.isBlocked)
                    continue; // 이미 처리했거나 이동 불가능한 타일은 무시

                // 대각선 이동이면 1.4, 아니면 1의 가중치 사용 (거리 계산 안정성을 위해 10배)
                float movementCost = IsDiagonal(currentTile, neighbor) ? 1.4f : 1f;
                int tempG = currentTile.G + Mathf.RoundToInt(movementCost * 10);

                // 이미 openList에 있는데 더 나은 경로가 아니라면 무시
                if (openList.Contains(neighbor) && tempG >= neighbor.G)
                    continue;

                // 더 나은 경로를 찾았을 경우 값 갱신
                neighbor.previousTile = currentTile;
                neighbor.G = tempG;
                neighbor.H = GetManhattanDistance(neighbor, endTile) * 10;

                if (!openList.Contains(neighbor))
                    openList.Add(neighbor);
            }
        }

        // 경로를 찾지 못한 경우
        return null;
    }

    /// <summary>
    /// 목표 타일에서 시작 타일까지 previousTile을 따라가며 경로를 재구성한다.
    /// </summary>
    private List<OverlayTile> ReconstructPath(OverlayTile endTile)
    {
        List<OverlayTile> path = new List<OverlayTile>();
        OverlayTile current = endTile;

        while (current != null)
        {
            path.Add(current);
            current = current.previousTile;
        }

        path.Reverse(); // 시작부터 끝까지 순서로 정렬
        return path;
    }

    /// <summary>
    /// 맨해튼 거리(가로 + 세로 거리)를 구한다. (대각선 이동일 경우 이 방법이 다소 부정확할 수 있음)
    /// 거리 계산 단위를 10으로 곱해서 정수 처리.
    /// </summary>
    private int GetManhattanDistance(OverlayTile a, OverlayTile b)
    {
        return Mathf.Abs(a.gridLocation.x - b.gridLocation.x) + Mathf.Abs(a.gridLocation.y - b.gridLocation.y);
    }

    /// <summary>
    /// 두 타일의 위치가 x와 y 모두 다르면 대각선 관계임.
    /// </summary>
    private bool IsDiagonal(OverlayTile a, OverlayTile b)
    {
        return a.gridLocation.x != b.gridLocation.x && a.gridLocation.y != b.gridLocation.y;
    }

    /// <summary>
    /// 현재 타일에서 이동 가능한 8방향(상하좌우 + 대각선) 이웃 타일을 반환
    /// </summary>
    private List<OverlayTile> GetNeighborTiles(OverlayTile tile)
    {
        List<OverlayTile> neighbors = new List<OverlayTile>();

        // 8방향 방향 벡터 설정
        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right,
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = new Vector2Int(tile.gridLocation.x + dir.x, tile.gridLocation.y + dir.y);

            // 맵 안에 존재하는 경우만 유효한 이웃으로 처리
            if (TilemapManager.Instance.map.ContainsKey(neighborPos))
            {
                OverlayTile neighborTile = TilemapManager.Instance.map[neighborPos].GetComponent<OverlayTile>();
                neighbors.Add(neighborTile);
            }
        }

        return neighbors;
    }
}
