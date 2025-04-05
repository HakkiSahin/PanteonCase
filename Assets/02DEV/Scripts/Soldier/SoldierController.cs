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

    public void Fire(Vector2 targetPosition)
    {
        Debug.unityLogger.Log(_currentSoldier + "   "+ targetPosition);
        if (_currentSoldier != null)
        {
            _currentSoldier.Fire(targetPosition);
            //_currentSoldier = null;
        }
    }

    
}
