using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldierFactory : Factory
{
    [SerializeField]private CommonSoldier commonSoldierPrefab;
    
    [SerializeField] private GridSystem gridSystem;
    public override ISoldier CreateSoldier(Vector3 position, Vector2Int currentIndex)
    {
        GameObject soldier = ObjectPool.Instance.Get(commonSoldierPrefab.gameObject, PoolType.Soldiers, position);
        CommonSoldier newSoldier = soldier.GetComponent<CommonSoldier>();
        
        newSoldier.currentIndex = currentIndex;
        newSoldier.Initialize(gridSystem);
        
        return newSoldier;
    }
}
