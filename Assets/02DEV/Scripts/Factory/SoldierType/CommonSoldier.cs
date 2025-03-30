using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSoldier : MonoBehaviour, ISoldier
{
   

    public string SoldierName { get; set; }
    
    public Vector3 startPosition;
    
    
    public void Initialize()
    {
        Debug.Log("Common Soldier Initialize");
    }
}
