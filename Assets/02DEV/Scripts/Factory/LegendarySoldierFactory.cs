using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySoldierFactory : Factory
{
    [SerializeField]private LegendarySoldier legendarySoldierPrefab;
    [SerializeField] private GridSystem gridSystem;
    public override ISoldier CreateSoldier(Vector3 position, Vector2Int currentIndex)
    {
        GameObject soldier = ObjectPool.Instance.Get(legendarySoldierPrefab.gameObject, PoolType.Soldiers, position);
        
        LegendarySoldier newSoldier = soldier.GetComponent<LegendarySoldier>();
        newSoldier.currentIndex = currentIndex;
        newSoldier.Initialize(gridSystem);
        
        return newSoldier;
    }
}
