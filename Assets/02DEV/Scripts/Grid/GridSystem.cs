using System;
using Unity.VisualScripting;
using UnityEngine;

public struct GridCless
{
    public Vector3 position;
    public bool isFull;
}
public class GridSystem : MonoBehaviour
{
    public int columns = 20;
    public int rows = 30;
    public float cellSize = 0.32f;
    public float cellSizex = 0.33f;
    public Sprite sprite;
    private GridCless[,] gridCells;

    void Start()
    {
        gridCells = new GridCless[columns, rows];
        DrawGrid();
    }


    void DrawGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 position = new Vector2(x * cellSizex, y * cellSizex);
                DrawCell(position);
                gridCells[x, y] = new GridCless { position = position, isFull = false };
            }
        }

       // transform.position = Vector3.zero;
    }

    void DrawCell(Vector2 position)
    {
        GameObject cell = new GameObject("Cell");
        cell.transform.SetParent(this.transform);
        cell.transform.localPosition = position;
        cell.transform.localScale = Vector3.one * cellSize;
      

        SpriteRenderer renderer = cell.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = new Color(1, 1, 1, 0.3f);
    }


    public Vector2 FindNearestCell(Vector2 position)
    {
        // Pozisyonu grid index'ine çevir
        Vector2 cellIdx = (position )  / cellSize ;

        // FloorToInt kullanarak indeks al
        Vector2Int cellIdxInt = new Vector2Int(Mathf.FloorToInt(cellIdx.x), Mathf.FloorToInt(cellIdx.y));

        // Clamp ile sınırları aşmasını engelle
        cellIdxInt.x = Mathf.Clamp(cellIdxInt.x, 0, columns - 1);
        cellIdxInt.y = Mathf.Clamp(cellIdxInt.y, 0, rows - 1);

        Debug.Log($"Hücre Index: {cellIdxInt} - Pozisyon: {gridCells[cellIdxInt.x, cellIdxInt.y].position}");

        // İlgili hücre pozisyonunu döndür

        if (!gridCells[cellIdxInt.x, cellIdxInt.y].isFull)
        {
            gridCells[cellIdxInt.x, cellIdxInt.y].isFull = true; 
            return gridCells[cellIdxInt.x, cellIdxInt.y].position;
        }
        
        return Vector2.one*-1;
    }

  
}
