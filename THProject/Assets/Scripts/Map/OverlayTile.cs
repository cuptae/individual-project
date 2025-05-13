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

    PolygonCollider2D polygonCollider;

    LineRenderer lineRenderer;

    void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isOnMoveRange = true;
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        isOnMoveRange = false;
    }   

    public GameObject CheckIsOnObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f,~ LayerMask.GetMask("OverlayTile"));
        foreach (Collider2D collider in colliders)
        {
            isOnObject = true;
            return collider.gameObject;   
        }
        isOnObject = false;
        return null;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
    public void DrawTileOutline()
    {
        if (lineRenderer == null || polygonCollider == null) return;

        Vector2[] points = polygonCollider.points;
        lineRenderer.positionCount = points.Length + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        Vector3[] linePoints = new Vector3[points.Length + 1];
        for (int i = 0; i < points.Length; i++)
        {
            linePoints[i] = points[i];
        }
        linePoints[points.Length] = points[0]; // 닫기

        lineRenderer.SetPositions(linePoints);

        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public void EraseTileOutLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }
}
