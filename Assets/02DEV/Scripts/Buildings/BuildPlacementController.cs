using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPlacementController : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;
    
    private GridSystem _gridSystem;
    
    [SerializeField] Vector2 buildSize = new Vector2(4, 2);
    [SerializeField] SpriteRenderer buildRendere;
    [SerializeField] Buildings buildings;
    
    private Vector2 _currentLocation;
    
    
    void Start()
    {
        mainCamera = Camera.main;
        _gridSystem = FindObjectOfType<GridSystem>();
    }

    private void OnEnable()
    {
        isDragging = true;
    }

    private Rigidbody2D _myRigid; //We add rigidbody so that the buildings do not overlap each other until they are placed in a proper position.
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _myRigid  = transform.AddComponent<Rigidbody2D>();
            _myRigid.gravityScale = 0;
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DestroyImmediate(_myRigid);
            FindNearestCell();
            isDragging = false;
            
        }
    }

    //After the mouse is up, it finds the nearest cell and fixes us there
    private void FindNearestCell()
    {
        transform.SetParent(_gridSystem.transform);
        Vector2 placementPos = _gridSystem.FindNearestCell(transform.localPosition , buildSize); //To find the closest cell position and to change the state of empty cells according to the size of the building, we send our values to the method

        if (placementPos.x > -1)
        {
            buildings.enabled=true;
            buildings.currentLocation = placementPos;
            this.enabled =false;
            
            _currentLocation = placementPos;
            transform.localPosition = _currentLocation;
            EventBus<BuildPlacementEvent>.Emit(this.gameObject, new BuildPlacementEvent());
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        else
        {
            Debug.Log("Wrong Cell");
        }
        
    }
        
    private bool isStay;
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Build") && !isStay && isDragging)
        {
            isStay = true;
            buildRendere.color = Color.red; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Build"))
        {
            isStay = false;
            buildRendere.color = Color.white; 
        }

    }
     
}
