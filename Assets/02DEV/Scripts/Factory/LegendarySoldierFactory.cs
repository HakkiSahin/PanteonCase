using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySoldierFactory : Factory
{
    [SerializeField]private LegendarySoldier legendarySoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        GameObject soldier = ObjectPool.Instance.Get(legendarySoldierPrefab.gameObject, PoolType.Soldiers, position);
        LegendarySoldier newSoldier = soldier.GetComponent<LegendarySoldier>();
        
        newSoldier.Initialize();
        
        return newSoldier;
    }
}
