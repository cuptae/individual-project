using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool isWalkable = true;
    public OverlayTile previousTile;
    public int gCost;
    public int hCost;
    public int FCost => gCost + hCost;
}
