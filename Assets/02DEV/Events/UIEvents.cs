using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

}
