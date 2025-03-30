using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

public class UnitCreateManager : MonoBehaviour
{
    
    private Vector3 _buildPosition;
    private void OnEnable()
    {
        EventBus<SetBuildPosEvent>.AddListener(BuildPos);
    }

    private void BuildPos(object sender, SetBuildPosEvent e)
    {
        _buildPosition = e.Position;
    }

    private void OnDisable()
    {
        EventBus<SetBuildPosEvent>.RemoveListener(BuildPos);
    }

    [SerializeField] private Factory[] factories;
    
    private Factory _factory;

    public void SpawnOnClick(int soldierIndex)
    {
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
        
        _factory.CreateSoldier(_buildPosition);
    }
}
