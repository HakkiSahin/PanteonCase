using EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Buildings : MonoBehaviour,IClickable
   {
      public string buildingName;
      public Sprite buildingImage;
      


      protected virtual void ShowInformationPanel()
      {
         Debug.Log("Buildings Information Panel");
         EventBus<ShowBuildingInfoEvent>.Emit(this, new ShowBuildingInfoEvent{BuildName = buildingName, BuildImage = buildingImage});
      }
     
      public void OnClick()
      {
         ShowInformationPanel();
      }
   }

