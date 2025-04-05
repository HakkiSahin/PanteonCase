using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Build Name and Build Image Set Event
public struct ShowBuildingInfoEvent
{
    public string BuildName;
    public Sprite BuildImage;
}

public struct ShowBuildingUnitEvent
{
    public List<Sprite> UnitImages;
}

public struct GetNearestCellEvent
{
    
}

public struct SetNearestCellEvent
{
    public Vector2 SpawnPosition;
    public Vector2Int CellIndex;
}


public struct CreateBuildEvent
{
    public int BuildIndex;
}


public struct BuildPlacementEvent
{
    
}