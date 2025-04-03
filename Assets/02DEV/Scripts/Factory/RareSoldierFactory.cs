using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldierFactory : Factory
{
    [SerializeField]private RareSoldier rareSoldierPrefab;
    [SerializeField] private GridSystem gridSystem;
    public override ISoldier CreateSoldier(Vector3 position, Vector2Int currentIndex)
    {
        GameObject soldier = ObjectPool.Instance.Get(rareSoldierPrefab.gameObject, PoolType.Soldiers, position);
        
        RareSoldier newSoldier = soldier.GetComponent<RareSoldier>();
        newSoldier.currentIndex = currentIndex;
        newSoldier.Initialize(gridSystem);
        
        return newSoldier;
    }
}
