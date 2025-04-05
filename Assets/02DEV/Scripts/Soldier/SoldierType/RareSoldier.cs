using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldier : MonoBehaviour, ISoldier, IClickable
{
    public string SoldierName { get; set; }
    public int SoldierDamage { get; set; }
    public Vector3 startPosition;
    public Vector2Int currentIndex;
    
    public void OnEnemyDeath()
    {
        ObjectPool.Instance.Return(gameObject, PoolType.Soldiers);
    }

    public Vector2Int CurrentPos { get; set; }
    public Pathfinding Pathfinding { get; set; }
    public HealthController HealthController { get; set; }
    
    [SerializeField] GameObject bulletPrefab;
    
    [SerializeField] private float moveSpeed = 5f;

    public void Initialize(GridSystem grid)
    {
        Pathfinding = new Pathfinding(grid.GridCells, grid.rows, grid.columns);
    }

    public void GetHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    [ContextMenu("Dead")]
    public void DeadSoldier()
    {
        ObjectPool.Instance.Return(gameObject, PoolType.Soldiers);
    }


    [ContextMenu("Test Character")]
    public void FollowPath(Vector2Int target)
    {
        Vector2Int startPos = currentIndex;
        Vector2Int targetPos = target;

        // Yol bul
        List<Cell> path = Pathfinding.FindPath(startPos, targetPos);

        if (path != null)
        {
            StartCoroutine(MoveAlongPath(transform, path, moveSpeed , target));
        }
        else
        {
            Debug.Log("Hedefe ulaşmak mümkün değil!");
        }
    }

    public void MovePosition(Vector2Int pos)
    {
        FollowPath(pos);
    }

    public void Fire(Vector2 targetPosition)
    {
        Debug.Log("firee");
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bullet = bulletGO.GetComponent<BulletController>();

        if (bullet != null)
        {
            bullet.Initialize(targetPosition, 5,transform);
        }
    }

    IEnumerator MoveAlongPath(Transform unit, List<Cell> path, float speed , Vector2Int target)
    {
        foreach (Cell step in path)
        {
            Vector3 targetPosition = step.transform.position; // GridCell'in world position'ı

            while (Vector3.Distance(unit.position, targetPosition) > 0.1f) // Hedefe yaklaşana kadar hareket et
            {
                unit.position = Vector3.MoveTowards(unit.position, targetPosition, speed * Time.deltaTime);
                yield return null; // Bir sonraki frame'e geç
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        currentIndex = target;
    }

    public void OnClick()
    {
        SoldierController.Instance.SelectedSoldier(this);
    }

    public void OnFire()
    {
        throw new System.NotImplementedException();
    }
}