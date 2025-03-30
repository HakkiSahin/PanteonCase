
    public interface ISoldier
    {
        public string SoldierName { get; set; }
        
        public void Initialize();
    }

    public enum SoldierType
    {
        Common=1,
        Rare=2 ,
        Legendary=3
    }

