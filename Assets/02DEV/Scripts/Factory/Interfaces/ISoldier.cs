
    using UnityEngine;

    public interface ISoldier : IFire
    {
        public string SoldierName { get; set; }
        
        public int SoldierDamage { get; set; }

        public void OnEnemyDeath();
        
        
        public Vector2Int CurrentPos  { get; set; }
        
        public Pathfinding Pathfinding { get; set; }
        
        public HealthController HealthController { get; set; }
       
        
        public void GetHit(float damage);
        public void Initialize(GridSystem grid);
        public void MovePosition(Vector2Int pos);

        public void Fire(Vector2 targetPosition);
        

    }

    public enum SoldierType
    {
        Common=1,
        Rare=2 ,
        Legendary=3
    }

