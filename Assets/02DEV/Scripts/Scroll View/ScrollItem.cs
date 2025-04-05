using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] InfiniteScroll  infiniteScroll;
    [SerializeField] int buildIndex;
    public void OnPointerDown(PointerEventData eventData)
    {
        infiniteScroll.SetIndex(buildIndex);
    }
}