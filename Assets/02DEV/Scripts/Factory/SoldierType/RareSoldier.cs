using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSoldier : MonoBehaviour , ISoldier
{

    public string SoldierName { get; set; }
    public Vector3 startPosition;
    public void Initialize()
    {
        Debug.Log("RareSoldier Initialize");
    }
    
    [ContextMenu("Dead")]
    public void DeadSoldier()
    {
        ObjectPool.Instance.Return(gameObject, PoolType.Soldiers);
    }
}
