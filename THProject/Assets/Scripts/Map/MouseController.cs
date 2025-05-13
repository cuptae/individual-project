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
    public Mercenary curMercenary; // 병사 프리팹
    public OverlayTile overlayTile; // 마우스 오버레이 타일
    public MouseMode curMode = MouseMode.MercenarySpawn; // 마우스 모드

    

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        tileHighlight = Resources.Load("Prefabs/TileHighlight") as GameObject;
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
                    curMercenary.MoveToTile(overlayTile);
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

        public void MercenarySpawn()
        {
            if(curMode == MouseMode.MercenarySpawn&&overlayTile.isOnObject == false)
            {
                curMercenary = Instantiate(curMercenary, overlayTile.transform.position, Quaternion.identity);
                curMercenary.currentTile = overlayTile.GetComponent<OverlayTile>();
                curMercenary.currentTile.gridLocation = overlayTile.GetComponent<OverlayTile>().gridLocation;
                curMercenary.currentTile.CheckIsOnObject();
                curMercenary.ShowMoveRange();
            }
        }

        public void ChangeMouseMode(MouseMode mode)
        {
            curMode = mode;
        }
}
