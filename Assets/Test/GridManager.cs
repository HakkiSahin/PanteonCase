using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Node
{
    public Vector2Int Position;
    public bool IsObstacle;
    public Node Parent;
    public int G, H;
    public int F => G + H;
    
    public Node(Vector2Int position, bool isObstacle)
    {
        Position = position;
        IsObstacle = isObstacle;
    }
}

public class GridManager : MonoBehaviour
{
    public int width, height;
    public float cellSize = 0.32f;
    public float spacing = 0.01f;
    public GameObject cellPrefab;
    public GameObject soldierPrefab;  
    private Node[,] grid;
    
    
    private SoldierPathFollower currentSoldier;
    private AStarPathfinder pathfinder;

 

    void GenerateGrid()
    {
        grid = new Node[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isObstacle = Random.value < 0.2f; // Rastgele engeller
                grid[x, y] = new Node(new Vector2Int(x, y), isObstacle);
                SpawnCell(x, y, isObstacle);
            }
        }
    }

    void SpawnCell(int x, int y, bool isObstacle)
    {
        float xPos = x * (cellSize + spacing);
        float yPos = y * (cellSize + spacing);
        Vector2 worldPos = new Vector2(xPos, yPos);
        GameObject cell = Instantiate(cellPrefab, worldPos, Quaternion.identity, transform);
        if (isObstacle)
            cell.GetComponent<Renderer>().material.color = Color.red;
    }
 
    [ContextMenu("Test")]
    public void Test()
    {
        Vector2 targetWorldPos = new Vector2(4.95f, 4.95f);
        OnCellClicked(targetWorldPos);
    }

    public void OnCellClicked(Vector2 targetPosition)
    {
        // Askeri hedefe yönlendir
        if (currentSoldier != null)
        {
            currentSoldier.MoveTo(targetPosition);
        }
    }
}

