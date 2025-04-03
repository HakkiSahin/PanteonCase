using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

public class UnitCreateManager : MonoBehaviour
{
    private Vector3 _buildPosition;
    private Vector2Int _currentIndex;
    private void OnEnable()
    {
        EventBus<SetBuildPosEvent>.AddListener(BuildPos);
        EventBus<SetNearestCellEvent>.AddListener(SetSpawnPosition);
    }

    private void BuildPos(object sender, SetBuildPosEvent e)
    {
        _buildPosition = e.Position;
    }

    private void OnDisable()
    {
        EventBus<SetBuildPosEvent>.RemoveListener(BuildPos);
        EventBus<SetNearestCellEvent>.RemoveListener(SetSpawnPosition);
    }

    [SerializeField] private Factory[] factories;
    
    private Factory _factory;

    public void SpawnOnClick(int soldierIndex)
    {
        EventBus<GetNearestCellEvent>.Emit(this, new GetNearestCellEvent());
        
        switch (soldierIndex)
        {
            case 1:
                _factory = factories[0];
                break;
            case 2:
                _factory = factories[1];
                break;
            case 3:
                _factory = factories[2];
                break;
            
        }
        
        _factory.CreateSoldier(_buildPosition , _currentIndex);
    }
    
    private void SetSpawnPosition(object sender, SetNearestCellEvent e)
    {
        _buildPosition = e.SpawnPosition;
        _currentIndex = e.CellIndex;
    }
}

public class ActiveBuilding
{
    private static ActiveBuilding _instance;
    public static ActiveBuilding Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ActiveBuilding();
            }
            return _instance;
        }
    }

    private GameObject _activeBuild;

    public void SetActiveBuilding(GameObject build)
    {
        _activeBuild = build;
    }

    public GameObject GetActiveBuild()
    {
        return _activeBuild;
    }
}