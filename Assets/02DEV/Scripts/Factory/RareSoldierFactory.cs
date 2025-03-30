using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldierFactory : Factory
{
    [SerializeField]private RareSoldier rareSoldierPrefab;
    public override ISoldier CreateSoldier(Vector3 position)
    {
        GameObject soldierInstance = Instantiate(rareSoldierPrefab.gameObject, position, Quaternion.identity);
        RareSoldier soldier = soldierInstance.GetComponent<RareSoldier>();
        
        soldier.Initialize();
        
        return soldier;
    }
}
