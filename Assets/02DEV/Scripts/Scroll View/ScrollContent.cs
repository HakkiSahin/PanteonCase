using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// Her bir öğenin birbirinden ne kadar uzakta olduğu.
    /// </summary>
    public float ItemSpacing { get { return itemSpacing; } }

    /// <summary>
    /// Solundan ve sağından öğelerin ne kadar girintili olduğu.
    /// </summary>
    public float HorizontalMargin { get { return horizontalMargin; } }

    /// <summary>
    /// Üstünden ve altından öğelerin ne kadar girintili olduğu.
    /// </summary>
    public float VerticalMargin { get { return verticalMargin; } }

    /// <summary>
    /// Yatay yönlendirme
    /// </summary>
    public bool Horizontal { get { return horizontal; } }

    /// <summary>
    /// Dikey yönlendirme
    /// </summary>
    public bool Vertical { get { return vertical; } }

    /// <summary>
    /// Scroll genişliği
    /// </summary>
    public float Width { get { return width; } }

    /// <summary>
    /// Scroll yüksekliği
    /// </summary>
    public float Height { get { return height; } }

    /// <summary>
    /// Her ögenin genişliği
    /// </summary>
    public float ChildWidth { get { return childWidth; } }

    /// <summary>
    /// Her ögenin yüksekliği
    /// </summary>
    public float ChildHeight { get { return childHeight; } }

    #endregion

    #region Private Members

    /// <summary>
    /// Scrollun RectTransformu
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Srcoll ögelerinin RectTransformları
    /// </summary>
    private RectTransform[] rtChildren;

    /// <summary>
    /// Parent yüksekliği ve genişliği
    /// </summary>
    private float width, height;

    /// <summary>
    /// GameObjelerin yüksekliği ve genişliği
    /// </summary>
    private float childWidth, childHeight;

    /// <summary>
    /// GameObjelerin uzaklığı
    /// </summary>
    [SerializeField]
    private float itemSpacing;

    /// <summary>
    /// GameObjelerin girintiliği
    /// </summary>
    [SerializeField]
    private float horizontalMargin, verticalMargin;

    /// <summary>
    /// Scroll dikey mi yatay mı?
    /// </summary>
    [SerializeField]
    private bool horizontal, vertical;

    #endregion

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        // Sağdan ve soldan kenarlar
        width = rectTransform.rect.width - (2 * horizontalMargin);

        // Üstten ve alttan kenarları hesaplar
        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;
        childHeight = rtChildren[0].rect.height;

        if (vertical)
            InitializeContentVertical();
    }


    /// <summary>
    /// Sınırsız scrollu başlatır
    /// </summary>
    private void InitializeContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight + itemSpacing);
            rtChildren[i].localPosition = childPos;
        }
    }
}
