using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : Buildings
{
   [SerializeField] private List<Sprite> units  = new();

   protected override void ShowInformationPanel()
   {
      base.ShowInformationPanel();
      EventBus<ShowBuildingUnitEvent>.Emit(this, new ShowBuildingUnitEvent{ UnitImages = units});
   }
}
