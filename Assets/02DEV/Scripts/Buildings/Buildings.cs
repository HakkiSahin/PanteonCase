using System;
using EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Buildings : MonoBehaviour,IClickable
   {
      public string buildingName;
      public Sprite buildingImage;
      public bool  isPlaced;
      public Vector2 currentLocation;
      private void Start()
      {
         this.enabled =false;
      }

      protected virtual void ShowInformationPanel()
      {
         ActiveBuilding.Instance.SetActiveBuilding(gameObject);
         EventBus<ShowBuildingInfoEvent>.Emit(this, new ShowBuildingInfoEvent{BuildName = buildingName, BuildImage = buildingImage});
      }
     
      public void OnClick()
      {
         if (isPlaced) ShowInformationPanel();
      }
   }

