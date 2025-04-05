using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pathfinding 
{

    private Cell[,] grid;
    private int gridWidth, gridHeight;

    // Directions used for movement (including diagonals)
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0),
        new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
    };

    // Constructor to initialize the grid and its dimensions
    public Pathfinding(Cell[,] grid, int width, int height)
    {
        this.grid = grid;
        this.gridWidth = width;
        this.gridHeight = height;
    }

    // Finds a path from start to target using A* algorithm
    public List<Cell> FindPath(Vector2Int start, Vector2Int target)
    {
        if (grid[target.x, target.y].GetFull()) return null;

        SortedList<float, Queue<Vector2Int>> openSet = new SortedList<float, Queue<Vector2Int>>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
        Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, target);
        Enqueue(openSet, start, fScore[start]);

        while (openSet.Count > 0)
        {
            Vector2Int current = Dequeue(openSet);

            // If target reached, build the path
            if (current == target)
                return ReconstructPath(cameFrom, current);

            closedSet.Add(current);

            foreach (var direction in directions)
            {
                Vector2Int neighbor = current + direction;

                if (!IsInBounds(neighbor) || grid[neighbor.x, neighbor.y].GetFull() || closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbor);

                // If new path to neighbor is better, or neighbor not evaluated yet
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, target);

                    Enqueue(openSet, neighbor, fScore[neighbor]);
                }
            }
        }
        return null;
    }

    // Builds the path from target back to start using the cameFrom dictionary
    private List<Cell> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Cell> path = new List<Cell>();

        while (cameFrom.ContainsKey(current))
        {
            path.Add(grid[current.x, current.y]); // GridCells nesnesini ekliyoruz
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
    }
    // Checks if a position is within the grid bounds
    private bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < gridWidth && pos.y < gridHeight;
    }

    // Heuristic function using Euclidean distance
    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Vector2.Distance(a, b);
    }

    // Adds a node to the open set with a priority
    private void Enqueue(SortedList<float, Queue<Vector2Int>> openSet, Vector2Int node, float priority)
    {
        if (!openSet.ContainsKey(priority))
            openSet[priority] = new Queue<Vector2Int>();

        openSet[priority].Enqueue(node);
    }
    
    // Retrieves and removes the node with the lowest priority
    private Vector2Int Dequeue(SortedList<float, Queue<Vector2Int>> openSet)
    {
        float firstKey = openSet.Keys[0];
        Vector2Int node = openSet[firstKey].Dequeue();

        if (openSet[firstKey].Count == 0)
            openSet.Remove(firstKey);

        return node;
    }
}
