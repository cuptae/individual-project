using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding
{
    public List<OverlayTile> FindPath(OverlayTile startTile, OverlayTile endTile)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        HashSet<OverlayTile> closedList = new HashSet<OverlayTile>();

        openList.Add(startTile);
        startTile.G = 0;
        startTile.H = GetManhattanDistance(startTile, endTile);
        startTile.previousTile = null;

        while (openList.Count > 0)
        {
            OverlayTile currentTile = openList.OrderBy(t => t.F).First();

            if (currentTile == endTile)
            {
                return ReconstructPath(endTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (var neighbor in GetNeighborTiles(currentTile))
            {
                if (closedList.Contains(neighbor) || neighbor.isBlocked) continue;

                int tentativeG = currentTile.G + 1;
                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeG >= neighbor.G)
                {
                    continue;
                }

                neighbor.previousTile = currentTile;
                neighbor.G = tentativeG;
                neighbor.H = GetManhattanDistance(neighbor, endTile);
            }
        }

        return null; // 경로 없음
    }

    private List<OverlayTile> ReconstructPath(OverlayTile endTile)
    {
        List<OverlayTile> path = new List<OverlayTile>();
        OverlayTile current = endTile;
        while (current != null)
        {
            path.Add(current);
            current = current.previousTile;
        }
        path.Reverse();
        return path;
    }

    private int GetManhattanDistance(OverlayTile a, OverlayTile b)
    {
        return Mathf.Abs(a.gridLocation.x - b.gridLocation.x) + Mathf.Abs(a.gridLocation.y - b.gridLocation.y);
    }

    private List<OverlayTile> GetNeighborTiles(OverlayTile tile)
    {
        List<OverlayTile> neighbors = new List<OverlayTile>();
        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = new Vector2Int(tile.gridLocation.x + dir.x, tile.gridLocation.y + dir.y);
            if (TilemapManager.Instance.map.ContainsKey(neighborPos))
            {
                neighbors.Add(TilemapManager.Instance.map[neighborPos].GetComponent<OverlayTile>());
            }
        }

        return neighbors;
    }
}

