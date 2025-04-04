using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
   public GridSystem grids;

   private Pathfinding pathfinding;
   public float moveSpeed = 5f;

   [ContextMenu("Find Nearest")]
   public void Test()
   {
       // GridManager'dan grid bilgisini al
       GridSystem gridManager = FindObjectOfType<GridSystem>();
       pathfinding = new Pathfinding(gridManager.GridCells, gridManager.rows, gridManager.columns);

       
       
       Vector2Int startPos = new Vector2Int(0, 0);
       Vector2Int targetPos = new Vector2Int(14, 14);

       // Yol bul
       List<Cell> path = pathfinding.FindPath(startPos, targetPos);

       if (path != null)
       {
           StartCoroutine(MoveAlongPath(transform, path, moveSpeed));
       }
       else
       {
           Debug.Log("Hedefe ulaşmak mümkün değil!");
       }
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