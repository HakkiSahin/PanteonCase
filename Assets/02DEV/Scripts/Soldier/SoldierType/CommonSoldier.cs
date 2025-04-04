using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldier : MonoBehaviour, ISoldier, IClickable
{
    public string SoldierName { get; set; }
    [SerializeField] private float moveSpeed=5f;

    public Vector2Int currentIndex;

    public Vector2Int CurrentPos { get; set; }
    
    public Pathfinding Pathfinding { get; set; }
  

    public Vector3 startPosition;

  
    [ContextMenu("Dead")]
    public void DeadSoldier()
    {
        ObjectPool.Instance.Return(gameObject, PoolType.Soldiers);
    }

    public void Initialize(GridSystem grid)
    {
        Pathfinding = new Pathfinding(grid.GridCells, grid.rows, grid.columns);
    }
    
    public void MovePosition(Vector2Int pos)
    {
        FollowPath(pos);
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
            StartCoroutine(MoveAlongPath(transform, path, moveSpeed));
        }
        else
        {
            Debug.Log("Hedefe ulaşmak mümkün değil!");
        }
    }
    
    public void OnClick()
    {
        SoldierController.Instance.SelectedSoldier(this);
    }
    
    IEnumerator MoveAlongPath(Transform unit, List<Cell> path, float speed)
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
    }
}
