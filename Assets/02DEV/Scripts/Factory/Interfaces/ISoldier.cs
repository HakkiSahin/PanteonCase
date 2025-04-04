
    using UnityEngine;

    public interface ISoldier
    {
        public string SoldierName { get; set; }
        
        public void Initialize(GridSystem grid);
        
        public Vector2Int CurrentPos  { get; set; }
        
        public Pathfinding Pathfinding { get; set; }

        public void MovePosition(Vector2Int pos);
    }

    public enum SoldierType
    {
        Common=1,
        Rare=2 ,
        Legendary=3
    }

