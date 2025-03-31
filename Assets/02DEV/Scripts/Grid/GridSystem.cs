using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int columns = 20;
    public int rows = 30;
    public float cellSize = 0.32f;
    public Sprite sprite;

    private bool[,] gridCells; // Grid'in dolu/boş durumunu takip edecek
    private GameObject movingObject; // Hareket eden obje

    void Start()
    {
        gridCells = new bool[columns, rows]; // Başlangıçta tüm hücreler boş
        DrawGrid();
    }

    void Update()
    {
        // Mouse ile taşıma işlemi (sadece fare tıklanmışsa objeyi taşımaya başlar)
        if (movingObject != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movingObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);

            if (Input.GetMouseButtonUp(0)) // Fare bırakıldığında
            {
                TryPlaceObjectAtMouse();
            }
        }
    }

    void DrawGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 position = new Vector2(x * cellSize, y * cellSize) + new Vector2(-4.2f, -2.75f);
                DrawCell(position);
            }
        }
        transform.position = Vector3.zero;
    }

    void DrawCell(Vector2 position)
    {
        GameObject cell = new GameObject("Cell");
        cell.transform.position = position;
        cell.transform.localScale = Vector3.one * 0.3f;
        cell.transform.SetParent(this.transform);

        SpriteRenderer renderer = cell.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = new Color(1, 1, 1, 0.3f);
    }

    public void StartMovingObject(GameObject obj)
    {
        movingObject = obj;
    }

    void TryPlaceObjectAtMouse()
    {
        // Fare pozisyonunu grid sistemine göre yuvarla
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Grid hücresine yakın pozisyonu bul
        int gridX = Mathf.RoundToInt((mousePosition.x + 4.2f) / cellSize);
        int gridY = Mathf.RoundToInt((mousePosition.y + 2.75f) / cellSize);

        // Grid sınırlarını kontrol et
        gridX = Mathf.Clamp(gridX, 0, columns - 1);
        gridY = Mathf.Clamp(gridY, 0, rows - 1);

        // Obje yerleştirme
        if (TryPlaceObject(gridX, gridY, 1, 1)) // Örneğin, 1x1 boyutunda bir obje
        {
            Debug.Log($"Obje {gridX}, {gridY} pozisyonuna yerleştirildi.");
        }
        else
        {
            Debug.Log("Bu pozisyona yerleştirilemez!");
        }

        // Obje yerleştirildikten sonra taşıma bitiyor
        movingObject = null;
    }

    public bool TryPlaceObject(int startX, int startY, int width, int height)
    {
        // 1. Nesnenin sığabileceği bir alan olup olmadığını kontrol et
        if (!CanPlaceObject(startX, startY, width, height))
        {
            Debug.Log("Bu alana nesne yerleştirilemez! (Dolu veya sınır dışında)");
            return false;
        }

        // 2. Nesneyi yerleştir (hücreleri dolu olarak işaretle)
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                gridCells[x, y] = true;
            }
        }

        Debug.Log($"Nesne {startX}, {startY} konumuna ({width}x{height}) yerleştirildi.");
        return true;
    }

    private bool CanPlaceObject(int startX, int startY, int width, int height)
    {
        // Grid sınırlarını aşmama kontrolü
        if (startX < 0 || startY < 0 || startX + width > columns || startY + height > rows)
            return false;

        // Hücrelerin boş olup olmadığını kontrol et
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if (gridCells[x, y]) return false; // Eğer doluysa yerleştirilemez
            }
        }

        return true; // Tüm hücreler boşsa yerleştirme yapılabilir
    }
}
