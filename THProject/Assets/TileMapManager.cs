using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public Tilemap tilemap;
    public Vector2Int characterSpawnTile;

    void Awake()
    {

    }

    void Start()
    {   
        Vector3Int spawnTile = new Vector3Int(characterSpawnTile.x+1,characterSpawnTile.y+1,0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(spawnTile);
        Instantiate(characterPrefab,worldPos,Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            Vector3Int clickedTile = tilemap.WorldToCell(mouseWorldPos);
            Debug.Log("Clicked Tile Pos: " + clickedTile);
        }
    }
}

