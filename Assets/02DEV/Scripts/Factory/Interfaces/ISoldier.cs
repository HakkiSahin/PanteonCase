
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ISoldier 
    {
        public string SoldierName { get; set; }

        public void OnEnemyDeath();
        public Vector2Int CurrentPos  { get; set; } // Soldier Current Grid Index
        
        public Pathfinding Pathfinding { get; set; } // A* PathFinding
        
        public HealthController HealthController { get; set; } // Soldier HealthController
       
        
        public void GetHit(float damage);
        public void Initialize(GridSystem grid);
        public void MovePosition(Vector2Int pos);

        public void Fire(Vector2 targetPosition);

        IEnumerator MoveAlongPath(Transform unit, List<Cell> path, float speed, Vector2Int target);
    }

    public enum SoldierType
    {
        Common=1,
        Rare=2 ,
        Legendary=3
    }

