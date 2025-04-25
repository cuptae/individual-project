using System.Linq;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    public GameObject tileHighlight; // 커서 오브젝트
    public GameObject mercenaryPrefab; // 병사 프리팹
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        tileHighlight = Resources.Load("Prefabs/TileHighlight") as GameObject;
        tileHighlight = Instantiate(tileHighlight, Vector3.zero, Quaternion.identity);
    }

        // Update is called once per frame
        void LateUpdate()
        {
            //TileHighlight();
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                GameObject overlayTile = hit.Value.collider.gameObject;
                tileHighlight.transform.position = overlayTile.transform.position;
                tileHighlight.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

                if (Input.GetMouseButtonDown(0))
                {
                    MercenarySpawn();
                }
            }
        }

        public RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

            if(hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }
        public void MercenarySpawn()
        {
            Instantiate(mercenaryPrefab, tileHighlight.transform.position, Quaternion.identity);
        }








        // public void TileHighlight()
        // {
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector3Int girdPos = TilemapManager.Instance.tileMap.WorldToCell(mousePos);
        //     Vector2Int girdPos2D = new Vector2Int(girdPos.x, girdPos.y);
        //     //Vector3 worldPos = TilemapManager.Instance.tileMap.GetCellCenterWorld(girdPos);
        //     Debug.Log($"mosuePos{mousePos} girdPos:{girdPos}");
            
        //     Vector3 yoffset = new Vector3(0, 2.5f, 0);
        //     if(TilemapManager.Instance.map.ContainsKey(girdPos2D))
        //     {
        //        tileHighlight.transform.position = TilemapManager.Instance.map[girdPos2D].transform.position-yoffset;
        //     }
        // }
}
