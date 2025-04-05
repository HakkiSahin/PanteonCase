using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelPresenter : MonoBehaviour
{
    [SerializeField]private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingName;
    
    [SerializeField] List<Image> unitImages;
    [SerializeField] GameObject menu;
 
    private void OnEnable()
    {
        EventBus<ShowBuildingInfoEvent>.AddListener(GetBuildingInfo);
        EventBus<ShowBuildingUnitEvent>.AddListener(GetUnitInfo);
    }

    private void OnDisable()
    {
        EventBus<ShowBuildingInfoEvent>.RemoveListener(GetBuildingInfo);
        EventBus<ShowBuildingUnitEvent>.RemoveListener(GetUnitInfo);
    }
    
    private void GetUnitInfo(object sender, ShowBuildingUnitEvent e)
    {
        
        for (int i = 0; i < e.UnitImages.Count; i++)
        {
            ChangeActiveState(unitImages[i], true);
            unitImages[i].sprite = e.UnitImages[i];
        }
    }

    private void GetBuildingInfo(object sender, ShowBuildingInfoEvent e)
    {
        menu.SetActive(true);
        ClearData();
        buildingName.text = e.BuildName;
        ChangeActiveState(buildingImage, true);
        buildingImage.sprite = e.BuildImage;
        
    }
    private void ChangeActiveState(Image image , bool isOpen)
    {
        image.gameObject.SetActive(isOpen);
    }
    private void ClearData()
    {
        foreach (var img in unitImages)
        {
            ChangeActiveState(img, false);
        }
        ChangeActiveState(buildingImage, false);
        buildingName.text = "";
    }

    
}


