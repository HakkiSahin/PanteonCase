using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldierFactory : Factory
{
    [SerializeField]private CommonSoldier commonSoldierPrefab; // prefabs needed to create the soldiers
    
    [SerializeField] private GridSystem gridSystem; //to move the soldiers and place them in the cells. 
    public override ISoldier CreateSoldier(Vector3 position, Vector2Int currentIndex)
    {
        GameObject soldier = ObjectPool.Instance.Get(commonSoldierPrefab.gameObject, PoolType.Soldiers, position); //We send the necessary data to create in the Object Pool
        CommonSoldier newSoldier = soldier.GetComponent<CommonSoldier>();
        
        newSoldier.currentIndex = currentIndex;
        newSoldier.Initialize(gridSystem); 
        
        return newSoldier;
    }
}
