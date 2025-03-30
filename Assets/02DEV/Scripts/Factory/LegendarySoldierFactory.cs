using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySoldierFactory : Factory
{
    [SerializeField]private LegendarySoldier rareSoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        GameObject soldierInstance = Instantiate(rareSoldierPrefab.gameObject, position, Quaternion.identity);
        LegendarySoldier soldier = soldierInstance.GetComponent<LegendarySoldier>();
        
        soldier.Initialize();
        
        return soldier;
    }
}
