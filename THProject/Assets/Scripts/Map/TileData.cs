using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Normal,
    Hill,
    Wall,

}


public class TileData : TileBase
{    
    public Vector3Int GridPosition;
    public TileType Type;

    public bool IsWalkable => Type != TileType.Wall;

    public float MovementCost
    {
        get
        {
            switch (Type)
            {
                case TileType.Normal: return 1f;
                case TileType.Hill: return 2f;
                case TileType.Wall: return Mathf.Infinity;
                default: return 1f;
            }
        }
    }
}
