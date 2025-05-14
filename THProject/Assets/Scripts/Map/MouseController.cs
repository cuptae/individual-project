using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using UnityEditor.Overlays;
using UnityEngine;


public enum MouseMode
{
    MercenarySpawn = 1,
    MercenaryMove,
}

public class MouseController : MonoBehaviour
{
    public GameObject tileHighlight; // 커서 오브젝트
    public Mercenary mercenary; // 병사 프리팹
    public OverlayTile overlayTile; // 마우스 오버레이 타일
    public Vector2Int gridLocation; // 그리드 위치
    public MouseMode curMode = MouseMode.MercenarySpawn; // 마우스 모드

    

    void Awake()
    {
        tileHighlight = Resources.Load("Prefabs/TileHighlight") as GameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        tileHighlight = Instantiate(tileHighlight, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            ChangeMouseMode(MouseMode.MercenarySpawn);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeMouseMode(MouseMode.MercenaryMove);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if (hit.HasValue)
        {
            GameObject cusorOnOverlayTile = hit.Value.collider.gameObject;
            tileHighlight.transform.position = cusorOnOverlayTile.transform.position;
            tileHighlight.GetComponent<SpriteRenderer>().sortingOrder = cusorOnOverlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if(Input.GetMouseButtonDown(0))
            {
              overlayTile = cusorOnOverlayTile.GetComponent<OverlayTile>();
              switch(curMode)
              {
                case MouseMode.MercenarySpawn:
                    MercenarySpawn();
                    break;
                case MouseMode.MercenaryMove:
                    MercenaryManager.Instance.curMercenary.MoveToTile(overlayTile);
                    break;
              }   
            }
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero,Mathf.Infinity, LayerMask.GetMask("OverlayTile"));

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public void MercenarySpawn()//Test
    {
        if(curMode == MouseMode.MercenarySpawn&&overlayTile.isOnObject == false)
        {
            mercenary = Instantiate(mercenary, overlayTile.transform.position, Quaternion.identity);
            mercenary.currentTile = overlayTile.GetComponent<OverlayTile>();
            mercenary.currentTile.gridLocation = overlayTile.GetComponent<OverlayTile>().gridLocation;
            mercenary.currentTile.CheckIsOnObject();
            MercenaryManager.Instance.AddMercenary(mercenary);
        }
    }

        public void ChangeMouseMode(MouseMode mode)
        {
            curMode = mode;
        }



}
