using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [System.Serializable]
// public class MercenaryData
// {
//     public int maxHp;
//     public int attackPower;
//     public int moverange;
//     public int attackRange;
// }

public class Mercenary : MonoBehaviour,IDamageable
{
    //public MercenaryData data; // 용병 데이터
    public MercenaryDataSO dataSO; // 용병 데이터 스크립터블 오브젝트
    public OverlayTile currentTile; // 현재 타일
    Pathfinding pathfinding; // 경로 탐색기
    //MouseController mouseController; // 마우스 컨트롤러
    public bool isSelected{ get{ return MercenaryManager.Instance.curMercenary == this; } }
    private bool isMoving = false; // 이동 중 여부
    public int curHp;
    void Awake()
    {
        //mouseController = GameObject.FindWithTag("MouseCtrl").GetComponent<MouseController>();
        pathfinding = new Pathfinding();
        curHp = dataSO.maxHp;
    }
    
    public void ShowMoveRange()
    {
        Queue<OverlayTile> frontier = new Queue<OverlayTile>();
        Dictionary<OverlayTile, int> visited = new Dictionary<OverlayTile, int>();

        OverlayTile startTile = currentTile;
        frontier.Enqueue(startTile);
        visited[startTile] = 0;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            int currentCost = visited[current];

            current.ShowTile();

            if (currentCost >= dataSO.moverange)
                continue;

            foreach (var neighbor in TilemapManager.Instance.Get4DirectionalNeighbors(current))
            {
                if (neighbor.isBlocked) continue;
                if (visited.ContainsKey(neighbor)) continue;

                visited[neighbor] = currentCost + 1;
                frontier.Enqueue(neighbor);
            }
        }
        //ShowAttackRange();
    }

    public void HideMoveRange()
    {
        foreach (var tile in TilemapManager.Instance.map.Values)
        {
            var overlayTile = tile.GetComponent<OverlayTile>();
            if (overlayTile.isOnMoveRange)
            {
                overlayTile.HideTile();
                overlayTile.EraseTileOutLine();
            }
        }
    }

    public void MoveToTile(OverlayTile targetTile)
    {
        if (isMoving) return; // 이미 이동 중이면 무시
        if (!targetTile.isOnMoveRange) return;
        if(targetTile.isOnObject)
        {
            GameObject targetObject = targetTile.CheckIsOnObject();
            // if(targetObject != null && targetObject.GetComponent<IDamageable>() is IDamageable damageable)
            // {
            //     Attack(damageable);
            //     return;
            // }
        }
        var path = pathfinding.FindPath(currentTile, targetTile);

        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
    }

    IEnumerator FollowPath(List<OverlayTile> path)
    {
        isMoving = true;
        OverlayTile previousTile = currentTile;
        foreach (var tile in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = tile.transform.position;
            float elapsedTime = 0f;
            float duration = 0.2f; // 이동 시간 (조절 가능)

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // 다음 프레임까지 대기
            }

            transform.position = endPos; // 보정
        }

        isMoving = false;
        previousTile.CheckIsOnObject();
        currentTile = path.Last();
        currentTile.CheckIsOnObject();
        HideMoveRange();

        ShowMoveRange();
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(dataSO.attackPower);
    }

    public void TakeDamage(int damage)
    {
        curHp -=damage;
        if(curHp<0)
        {
            Die();
        }
    }
    public void Heal(int healAmount)
    {
        curHp += healAmount;
    }
    public void Die()
    {
        Destroy(gameObject);
    }


    // public void ShowAttackRange()
    // {

    //     Queue<OverlayTile> frontier = new Queue<OverlayTile>();
    //     Dictionary<OverlayTile, int> visited = new Dictionary<OverlayTile, int>();

    //     OverlayTile startTile = currentTile;
    //     frontier.Enqueue(startTile);
    //     visited[startTile] = 0;

    //     while(frontier.Count>0)
    //     {
    //         var current = frontier.Dequeue();
    //         int currentCost = visited[current];

    //         current.DrawTileOutline();

    //         if(currentCost>=data.attackRange)
    //          continue;

    //         foreach(var neighbor in TilemapManager.Instance.Get4DirectionalNeighbors(current))
    //         {
    //             if(neighbor.isBlocked) continue;
    //             if (visited.ContainsKey(neighbor)) continue;

    //             visited[neighbor] = currentCost + 1;
    //             frontier.Enqueue(neighbor);
    //         }
    //     }
    // }

}
