using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : Singleton<SoldierController>
{
    private ISoldier _currentSoldier;

    public void SelectedSoldier(ISoldier soldier)
    {
        _currentSoldier = soldier;
    }

    public void MovePosition(Vector2Int pos)
    {
        if (_currentSoldier != null)
        {
            _currentSoldier.MovePosition(pos);
            _currentSoldier = null;
        }
        
    }
}
