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
    
    public Vector3 GetWorldPosition(Transform cellTransform)
    {
        // Grid objesinin transform'unu kullanarak yerel pozisyonu dünya pozisyonuna dönüştür.
        return cellTransform.TransformPoint(this.position);
    }
}
public class GridSystem : MonoBehaviour
{
    public int columns = 20;
    public int rows = 30;
    public float cellSize = 0.32f;
    public float cellSizex = 0.33f;
    public Sprite sprite;
    private GridCells[,] gridCells;

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
        gridCells = new GridCells[columns, rows];
        DrawGrid();
    }


    void DrawGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 position = new Vector2(x * cellSizex, y * cellSizex);
                Transform newTransform = DrawCell(position);
                gridCells[x, y] = new GridCells { position = position, isFull = false };
                gridCells[x, y].cellTransform = newTransform;
            }
        }
    }

    Transform DrawCell(Vector2 position)
    {
        GameObject cell = new GameObject("Cell");
        cell.transform.SetParent(this.transform);
        cell.transform.localPosition = position;
        cell.transform.localScale = Vector3.one * cellSize;
        
        
        SpriteRenderer renderer = cell.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = new Color(1, 1, 1, 0.3f);
        
        return cell.transform;
    }


    public Vector2 FindNearestCell(Vector2 position, Vector2 size)
    {
        // Pozisyonu grid index'ine çevir
        Vector2 cellIdx = (position )  / cellSize ;

        // FloorToInt kullanarak indeks al
        Vector2Int cellIdxInt = new Vector2Int(Mathf.FloorToInt(cellIdx.x), Mathf.FloorToInt(cellIdx.y));

        // Clamp ile sınırları aşmasını engelle
        cellIdxInt.x = Mathf.Clamp(cellIdxInt.x, 0, columns - 1);
        cellIdxInt.y = Mathf.Clamp(cellIdxInt.y, 0, rows - 1);

        Debug.Log($"Hücre Index: {cellIdxInt} - Pozisyon: {gridCells[cellIdxInt.x, cellIdxInt.y].position}");

        
        for (int x = cellIdxInt.x; x < cellIdxInt.x+ size.x; x++)
        {
            for (int y = cellIdxInt.y; y < cellIdxInt.y+ size.y; y++)
            {
                if (gridCells[x, y].isFull)
                {
                    Debug.Log($"{x},{y} bu satirlar doludur");
                    return Vector2.one*-1;
                }
            }
        }

            CellStateChange(cellIdxInt,size,true);
            return gridCells[cellIdxInt.x, cellIdxInt.y].position;
        
    }

    void CellStateChange(Vector2Int startGridPos, Vector2 endGridPos, bool state)
    {
        for (int x = startGridPos.x; x < startGridPos.x+ endGridPos.x; x++)
        {
            for (int y = startGridPos.y; y < startGridPos.y+ endGridPos.y; y++)
            {
                gridCells[x, y].isFull = state;
            }
        }
    }
    
    
    private void NearCellForSoldier(object sender, FindNearCellEvent e)
    {

        Vector2 cellIdx = (e.Location )  / cellSizex ;
        
        Vector2Int cellIdxInt = new Vector2Int(Mathf.FloorToInt(cellIdx.x), Mathf.FloorToInt(cellIdx.y));
        
        BFSFindEmptySpace(cellIdxInt);
     
    }
    
        
    private void BFSFindEmptySpace(Vector2Int start)
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

                        if (!gridCells[newX, newY].isFull) // Yeni pozisyonda alan boşsa
                        {
                            Debug.Log(gridCells[newX, newY].position);
                            EventBus<SetNearestCellEvent>.Emit(this, new SetNearestCellEvent{ SpawnPosition =  gridCells[newX, newY].cellTransform.position });
                            gridCells[newX, newY].isFull = true;
                            return; 
                        }
                    }
                }
            }
        }

        Debug.Log("fuck yasin");// Eğer hiçbir uygun yer bulunmazsa
    }
   
    }
  

