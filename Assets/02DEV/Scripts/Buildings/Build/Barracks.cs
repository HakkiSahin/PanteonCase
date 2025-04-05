using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : Buildings
{
    [SerializeField] private List<Sprite> units = new();
    [SerializeField] private BuildPlacementController testScript;

    private void OnEnable()
    {
        EventBus<GetNearestCellEvent>.AddListener(GetNearestCell);
        isPlaced = true;
    }

    private void OnDisable()
    {
        EventBus<GetNearestCellEvent>.RemoveListener(GetNearestCell);
    }

    protected override void ShowInformationPanel()
    {
        base.ShowInformationPanel();
        EventBus<ShowBuildingUnitEvent>.Emit(this, new ShowBuildingUnitEvent { UnitImages = units });
    }

    private void GetNearestCell(object sender, GetNearestCellEvent e)
    {
        if (ActiveBuilding.Instance.GetActiveBuild() == gameObject)
            EventBus<FindNearCellEvent>.Emit(this, new FindNearCellEvent { Location = currentLocation });
    }
}