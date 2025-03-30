using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySoldier : MonoBehaviour,ISoldier
{
    public string SoldierName { get; set; }
    public Vector3 startPosition;
    public void Initialize()
    {
        Debug.Log("LegendarySoldier Initialize");
    }
}
