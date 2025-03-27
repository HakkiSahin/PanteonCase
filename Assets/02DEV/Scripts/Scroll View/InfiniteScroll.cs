using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    #region Private Members

    /// <summary>
    /// Scroll Content
    /// </summary>
    [SerializeField]
    private ScrollContent scrollContent;

    /// <summary>
    /// Ögelerin hareketi
    /// </summary>
    [SerializeField]
    private float outOfBoundsThreshold;

    /// <summary>
    /// ScrollRect 
    /// </summary>
    private ScrollRect scrollRect;

    /// <summary>
    /// Son konum
    /// </summary>
    private Vector2 lastDragPosition;

    /// <summary>
    /// Yukarı mı yoksa aşağı mu kaydırma var
    /// </summary>
    private bool positiveDrag;

    #endregion

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    /// <summary>
    /// Sürüklemeye başlandığı an çağırılır.
    /// </summary>
    /// <param name="eventData">Sürüklenme verisi.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Scroll sürüklenirken çağırılır.
    /// </summary>
    /// <param name="eventData">Sürüklenme verisi</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.position.y > lastDragPosition.y;
        }
        else if (scrollContent.Horizontal)
        {
            positiveDrag = eventData.position.x > lastDragPosition.x;
        }

        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Eğer sürükleme fare tekerleği ile yapılırsa çalışır.
    /// </summary>
    /// <param name="eventData">Sürüklenme verisi</param>
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            //Kaydırmanın ne tarafa doğru olduğu hesaplanır.
            //eventData.scrollDelta.y buna göre sürüklenmenin nereye olduğu anlaşılır. 0 dan küçükse.
            positiveDrag = eventData.scrollDelta.y < 0;
        }
    }

    /// <summary>
    /// Scroll view sürüklenirken çağırılır.
    /// </summary>
    public void OnViewScroll()
    {
        if (scrollContent.Vertical)
        {
            HandleVerticalScroll();
        }
    }

    /// <summary>
    /// Kaydırma yönü dik çalışır.
    /// </summary>
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
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


    /// <summary>
    /// Content objelerinin sınır dışına çıkıp çıkmadığını kontrol eder.
    /// </summary>
    /// <param name="item">Kontrol edilen obje</param>
    private bool ReachedThreshold(Transform item)
    {
        if (scrollContent.Vertical)
        {
            float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        }
        else
        {
            float posXThreshold = transform.position.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
                item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
        }
    }
}
