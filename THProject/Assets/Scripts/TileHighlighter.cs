using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField]
    private GameObject highlight;
    [SerializeField]
    private OverlayTile overlayTile;

    private Vector3 lastHighlightPos;


    public GameObject mercenary;
    public bool isSpawn;


    void Awake()
    {
        tilemap = GameObject.FindWithTag("Map").GetComponent<Tilemap>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
            TileHighlight();
        if(Input.GetMouseButton(1))
            GetCharacter();
    }


    private void TileHighlight()
    {
        //마우스좌표를 월드 좌표로
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        //마우스 월드 좌표를 타일맵의 셀 좌표로
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

        //만약 마우스위치에 타일이 있다면
        if(tilemap.HasTile(cellPos))
        {
            //셀 위치의 중심좌표를 월드 좌표로
            highlight.transform.position  = tilemap.GetCellCenterWorld(cellPos)+TilemapManager.Instance.offset;
            lastHighlightPos = highlight.transform.position+TilemapManager.Instance.offset;
        }
        else
        {
            highlight.transform.position = lastHighlightPos;
        }
        Debug.Log(cellPos);
    }

    public void GetCharacter()
    {
        if(isSpawn)
            return;

        mercenary = Instantiate(mercenary);
        mercenary.transform.position = highlight.transform.position+TilemapManager.Instance.offset;
        isSpawn =true;
    }
}
