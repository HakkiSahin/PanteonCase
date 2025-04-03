using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldier : MonoBehaviour , ISoldier
{
    public string SoldierName { get; set; }
    public Vector3 startPosition;
    public Vector2Int currentIndex;
    public Vector2Int CurrentPos { get; set; }
    public Pathfinding Pathfinding { get; set; }
    public void Initialize(GridSystem grid)
    {
        Pathfinding = new Pathfinding(grid.GridCells, grid.rows, grid.columns);
    }

    [ContextMenu("Dead")]
    public void DeadSoldier()
    {
        ObjectPool.Instance.Return(gameObject, PoolType.Soldiers);
    }
}
