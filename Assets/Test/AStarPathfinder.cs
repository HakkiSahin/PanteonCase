// AStarPathfinder.cs
using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinder
{
    private Node[,] grid;
    private int width, height;
    
    public AStarPathfinder(Node[,] grid, int width, int height)
    {
        this.grid = grid;
        this.width = width;
        this.height = height;
    }
    
    public List<Node> FindPath(Vector2Int start, Vector2Int target)
    {
        Debug.Log("in here Find Path");
        
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        Node startNode = grid[start.x, start.y];
        Node targetNode = grid[target.x, target.y];
        
        openSet.Add(startNode);
        
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].F < currentNode.F || (openSet[i].F == currentNode.F && openSet[i].H < currentNode.H))
                {
                    currentNode = openSet[i];
                }
            }
            
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }
            
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (neighbor.IsObstacle || closedSet.Contains(neighbor))
                    continue;
                
                int newG = currentNode.G + 1;
                if (!openSet.Contains(neighbor) || newG < neighbor.G)
                {
                    neighbor.G = newG;
                    neighbor.H = Mathf.Abs(neighbor.Position.x - target.x) + Mathf.Abs(neighbor.Position.y - target.y);
                    neighbor.Parent = currentNode;
                    
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return new List<Node>();
    }
    
    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }
    
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        
        foreach (var dir in directions)
        {
            Vector2Int neighborPos = node.Position + dir;
            if (neighborPos.x >= 0 && neighborPos.x < width && neighborPos.y >= 0 && neighborPos.y < height)
            {
                neighbors.Add(grid[neighborPos.x, neighborPos.y]);
            }
        }
        return neighbors;
    }
}
