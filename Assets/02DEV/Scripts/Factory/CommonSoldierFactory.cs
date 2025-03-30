using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldierFactory : Factory
{
    [SerializeField]private CommonSoldier commonSoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        GameObject soldierInstance = Instantiate(commonSoldierPrefab.gameObject, position, Quaternion.identity);
        CommonSoldier soldier = soldierInstance.GetComponent<CommonSoldier>();
        
        soldier.Initialize();
        
        return soldier;
    }
}
