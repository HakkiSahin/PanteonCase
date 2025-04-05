using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

public class BuildCreateController : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings;

    private void OnEnable()
    {
        EventBus<CreateBuildEvent>.AddListener(CreateBuild);
    }
    private void OnDisable()
    {
        EventBus<CreateBuildEvent>.RemoveListener(CreateBuild);
    }
    
    private void CreateBuild(object sender, CreateBuildEvent e)
    {
        Debug.Log(e.BuildIndex + " CreateBuild");
        Instantiate(buildings[e.BuildIndex], Input.mousePosition, Quaternion.identity);
    }
}
