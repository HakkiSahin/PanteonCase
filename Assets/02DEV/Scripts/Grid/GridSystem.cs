using System;
using System.Collections.Generic;
using EventBus;
using Unity.VisualScripting;
using UnityEngine;

public struct GridCells
{
    public Vector3 position;
    public bool isFull;
    public Transform cellTransform;
}

public class GridSystem : MonoBehaviour
{
    public int columns = 20;
    public int rows = 30;
    public float cellSize = 0.32f;
    
    private static float _cellSizex = 0.33f;
   
    public Cell[,] GridCells;
    [SerializeField] GameObject cellPrefab;
    
    private void OnEnable()
    {
        EventBus<FindNearCellEvent>.AddListener(NearCellForSoldier);
    }

    private void OnDisable()
    {
        EventBus<FindNearCellEvent>.RemoveListener(NearCellForSoldier);
    }

    void Start()
    {
        GridCells = new Cell[columns, rows];
        DrawGrid();
      
    }


 

    void DrawGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 position = new Vector2(x * _cellSizex, y * _cellSizex);
                GridCells[x, y] = DrawCell(position);
            }
        }
    }

   private Cell DrawCell(Vector2 position)
    {
        Cell cell = Instantiate(cellPrefab, transform,true).GetComponent<Cell>();
        cell.transform.localPosition = position;
        cell.transform.localScale = Vector3.one * cellSize;
        cell.SetIndex(PositionToCellIndex(position));
        cell.SetIsFull(false);
        return cell;
    }


    public Vector2 FindNearestCell(Vector2 position, Vector2 size)
    {  // FloorToInt kullanarak indeks al
        Vector2Int cellIdxInt = PositionToCellIndex(position);
        
        // Clamp ile sınırları aşmasını engelle
        cellIdxInt.x = Mathf.Clamp(cellIdxInt.x, 0, columns - 1);
        cellIdxInt.y = Mathf.Clamp(cellIdxInt.y, 0, rows - 1);

     //   Debug.Log($"Hücre Index: {cellIdxInt} - Pozisyon: {GridCells[cellIdxInt.x, cellIdxInt.y].GetIndex()}");


        for (int x = cellIdxInt.x; x < cellIdxInt.x + size.x; x++)
        {
            for (int y = cellIdxInt.y; y < cellIdxInt.y + size.y; y++)
            {
                Debug.Log(x + "  " + y);
                if (GridCells[x, y].GetFull())
                {
                    Debug.Log($"{x},{y} bu satirlar doludur");
                    return Vector2.one * -1;
                }
            }
        }

        CellStateChange(cellIdxInt, size, true);
        return GridCells[cellIdxInt.x, cellIdxInt.y].transform.localPosition;
    }

    private Vector2Int  PositionToCellIndex(Vector2 position)
    {
        
        Vector2 cellIdx = (position) / _cellSizex;
        Vector2Int cellIdxInt = new Vector2Int(Mathf.FloorToInt(cellIdx.x), Mathf.FloorToInt(cellIdx.y));
        return cellIdxInt;
    }

    void CellStateChange(Vector2Int startGridPos, Vector2 endGridPos, bool state)
    {
        for (int x = startGridPos.x; x < startGridPos.x + endGridPos.x; x++)
        {
            for (int y = startGridPos.y; y < startGridPos.y + endGridPos.y; y++)
            {
                GridCells[x, y].SetIsFull(state);
            }
        }
    }


    private void NearCellForSoldier(object sender, FindNearCellEvent e)
    {
        Vector2Int cellIdxInt = PositionToCellIndex(e.Location);
        BfsFindEmptySpace(cellIdxInt);
    }


    private void BfsFindEmptySpace(Vector2Int start)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);

        int maxRange = Mathf.Max(columns, rows); // Genişleme mesafesi

        for (int range = 1; range <= maxRange; range++) // Her bir mesafeyi kontrol et
        {
            for (int dx = -range; dx <= range; dx++) // X eksenindeki hareket
            {
                for (int dy = -range; dy <= range; dy++) // Y eksenindeki hareket
                {
                    int newX = start.x + dx;
                    int newY = start.y + dy;

                    Vector2Int newPos = new Vector2Int(newX, newY);

                    // Eğer grid içinde ve ziyaret edilmemişse
                    if (newX >= 0 && newX < columns && newY >= 0 && newY < rows && !visited.Contains(newPos))
                    {
                        visited.Add(newPos);
                        queue.Enqueue(newPos);

                        if (!GridCells[newX, newY].GetFull()) // Yeni pozisyonda alan boşsa
                        {
                           // Debug.Log(GridCells[newX, newY].position);
                            EventBus<SetNearestCellEvent>.Emit(this,
                                new SetNearestCellEvent
                                {
                                    SpawnPosition = GridCells[newX, newY].transform.position,
                                    CellIndex = new Vector2Int(newX, newY)
                                });
                            GridCells[newX, newY].SetIsFull(true);
                            return;
                        }
                    }
                }
            }
        }

        Debug.Log("fuck yasin"); // Eğer hiçbir uygun yer bulunmazsa
    }

    //------ path finding
   
}