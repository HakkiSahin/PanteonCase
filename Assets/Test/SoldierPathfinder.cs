using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween kütüphanesi için

public class SoldierPathFollower : MonoBehaviour
{
    private AStarPathfinder pathfinder;
    private GridManager gridManager;

    public void Setup(GridManager grid, AStarPathfinder pathFinder)
    {
        gridManager = grid;
        pathfinder = pathFinder;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        Vector2Int start = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Vector2Int target = new Vector2Int((int)targetPosition.x, (int)targetPosition.y);

        List<Node> path = pathfinder.FindPath(start, target);
      
        if (path.Count > 0)
        {
            // Path'i DOTween ile takip et
            foreach (var node in path)
            {
               
                Vector2 worldPos = new Vector2(node.Position.x, node.Position.y);
                transform.DOMove(worldPos, 0.5f).SetEase(Ease.Linear); // Askere hareket ettir
            }
        }
    }
}