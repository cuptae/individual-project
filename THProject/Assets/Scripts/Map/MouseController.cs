using System.Linq;
using UnityEngine;


public enum MouseMode
{
    None,
    MercenarySpawn,
    MercenaryMove,
    Attack,
    Skill,
    Item,
    EndTurn
}

public class MouseController : MonoBehaviour
{
    public GameObject tileHighlight; // 커서 오브젝트
    public GameObject mercenaryPrefab; // 병사 프리팹
    public GameObject overlayTile; // 마우스 오버레이 타일
    public Vector3 offset = new Vector3(0, 0.25f, 0); // 타일 오프셋

    public MouseMode mouseMode = MouseMode.None; // 마우스 모드
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
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                GameObject overlayTile = hit.Value.collider.gameObject;
                this.overlayTile = overlayTile;
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
            if(mouseMode != MouseMode.MercenarySpawn)
            {
                return;
            }
            Instantiate(mercenaryPrefab, overlayTile.transform.position+offset, Quaternion.identity);
            mouseMode = MouseMode.MercenaryMove;
        }
}
