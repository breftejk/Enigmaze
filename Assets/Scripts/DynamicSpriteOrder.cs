using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOrderController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float yOffset = 0.0f;  // Customowe przesunięcie w osi Y dla każdego obiektu
    public bool runOnlyOnce = false;  // Czy aktualizować tylko raz czy ciągle

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (runOnlyOnce)
        {
            UpdateSortingOrder();
        }
    }

    void Update()
    {
        if (!runOnlyOnce)
        {
            UpdateSortingOrder();
        }
    }

    void UpdateSortingOrder()
    {
        // Obliczamy Order in Layer na podstawie pozycji y z uwzględnieniem przesunięcia yOffset
        // Dodanie 5000 zapewnia, że wszystkie wartości Order in Layer są dodatnie
        spriteRenderer.sortingOrder = (int)((-(transform.position.y + yOffset) * 100) + 5000);
    }
}