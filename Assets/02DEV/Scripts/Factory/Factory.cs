using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract ISoldier CreateSoldier( Vector3 position , Vector2Int currentIndex );
    
    
    
}
