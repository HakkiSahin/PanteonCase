using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;
    
    public GridSystem gridSystem;
    [SerializeField] Vector2 buildSize = new Vector2(4, 2);
    [SerializeField] SpriteRenderer buildRendere;
    
    [SerializeField] Barracks barracks;
    
    private Vector2 _currentLocation;
    void Start()
    {
        mainCamera = Camera.main;
    }

    private Rigidbody2D myRigid;
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            myRigid  = transform.AddComponent<Rigidbody2D>();
            myRigid.gravityScale = 0;
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DestroyImmediate(myRigid);
            FindNearestCell();
            isDragging = false;
            
        }
    }

    private void FindNearestCell()
    {
        transform.SetParent(gridSystem.transform);
        Vector2 placementPos = gridSystem.FindNearestCell(transform.localPosition , buildSize);

        if (placementPos.x > -1)
        {
            barracks.enabled=true;
            barracks.currentLocation = placementPos;
            this.enabled =false;
            
            _currentLocation = placementPos;
            transform.localPosition = _currentLocation;
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
