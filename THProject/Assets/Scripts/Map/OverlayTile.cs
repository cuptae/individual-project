using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;
    public int F { get {return G+H;}}
    public bool isBlocked = false;
    public bool isOnObject = false;
    public bool isOnMoveRange = false;
    public OverlayTile previousTile;

    public Vector3Int gridLocation;
    void Update()
    {

    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isOnMoveRange = true;

    }
}
