using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ControlClickObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            FireControler();
        }
    }

    private void FireControler()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("in here Right");
            if (hit.transform.CompareTag("Build") || hit.transform.CompareTag("Soldier"))
            {
                SoldierController.Instance.Fire(hit.transform.position);
            }
            
        }
    }

    private void ControlClickObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        { 
            hit.collider.GetComponent<IClickable>()?.OnClick();
            
        }
    }
    
    

}
