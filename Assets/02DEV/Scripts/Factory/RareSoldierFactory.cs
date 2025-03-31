using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldierFactory : Factory
{
    [SerializeField]private RareSoldier rareSoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        GameObject soldier = ObjectPool.Instance.Get(rareSoldierPrefab.gameObject, PoolType.Soldiers, position);
        RareSoldier newSoldier = soldier.GetComponent<RareSoldier>();
        
        newSoldier.Initialize();
        
        return newSoldier;
    }
}
