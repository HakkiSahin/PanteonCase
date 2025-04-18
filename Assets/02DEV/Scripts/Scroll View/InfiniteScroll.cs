using System;
using EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    private void OnEnable()
    {
        EventBus<BuildPlacementEvent>.AddListener(BuildPlacement);
    }

    private void OnDisable()
    {
        EventBus<BuildPlacementEvent>.RemoveListener(BuildPlacement);
    }


    [SerializeField] private ScrollContent scrollContent;
    [SerializeField] private float outOfBoundsThreshold;
    
    private ScrollRect scrollRect;
    
    private Vector2 _lastDragPosition;
    private Vector2 _firstDragPosition;
    private bool _positiveDrag;
    private int _selectedIndex;
    private bool _isCreate;
    
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDragPosition = eventData.position;
        _firstDragPosition = eventData.position;
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            
            _positiveDrag = eventData.position.y > _lastDragPosition.y;
        }
        else if (scrollContent.Horizontal)
        {
            _positiveDrag = eventData.position.x > _lastDragPosition.x;
           
        }

        if (_lastDragPosition.x - _firstDragPosition.x > 200f && !_isCreate)
        {
            _isCreate = true;
            EventBus<CreateBuildEvent>.Emit(this, new CreateBuildEvent{ BuildIndex = _selectedIndex });
        }
        
        _lastDragPosition = eventData.position;
    }

   
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            _positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            _positiveDrag = eventData.scrollDelta.y < 0;
        }
    }
    
    private void BuildPlacement(object sender, BuildPlacementEvent @event)
    {
        _isCreate = false;
    }
  
    public void OnViewScroll()
    {
        if (scrollContent.Vertical)
        {
            HandleVerticalScroll();
        }
    }


    private void HandleVerticalScroll()
    {
        int currItemIndex = _positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = _positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (_positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * 1.5f + scrollContent.ItemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }
   
    private bool ReachedThreshold(Transform item)
    {
        if (scrollContent.Vertical)
        {
            float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
            return _positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        }
        else
        {
            float posXThreshold = transform.position.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
            return _positiveDrag ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
                item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
        }
    }

    public void SetIndex(int index)
    {
        _isCreate = false;
        _selectedIndex = index;
    }
}
