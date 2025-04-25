using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{

    public GameObject overlayPrefab; //오버레이 프리팹
    public GameObject overlayContainer;//오버레이 컨테이너


    public Dictionary<Vector2Int, GameObject> map;//타일맵

    protected override void Awake()
    {
        base.Awake();
        map = new Dictionary<Vector2Int, GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var tileMaps = gameObject.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
        foreach(var tm in tileMaps)
        {
            //타일맵의 타일이 존재하는 공간
            BoundsInt bounds = tm.cellBounds;
            for (int z = bounds.max.z; z > bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tm.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {   
                            Debug.Log("Tile Found: " + tileLocation);
                            GameObject overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                            Vector3 cellWorldPosition = tm.GetCellCenterWorld(tileLocation);
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder;
                            map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }
    }
}

