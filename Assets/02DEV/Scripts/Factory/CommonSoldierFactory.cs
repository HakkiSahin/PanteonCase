using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldierFactory : Factory
{
    [SerializeField]private CommonSoldier commonSoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        // GameObject soldierInstance = Instantiate(commonSoldierPrefab.gameObject, position, Quaternion.identity);
        GameObject soldier = ObjectPool.Instance.Get(commonSoldierPrefab.gameObject, PoolType.Soldiers, position);
        CommonSoldier newSoldier = soldier.GetComponent<CommonSoldier>();
        
        newSoldier.Initialize();
        
        return newSoldier;
    }
}
