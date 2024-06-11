using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOrderController : MonoBehaviour
{
    // Referencja do komponentu SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Offset do przesunięcia wartości y w obliczeniach sortowania
    public float yOffset = 0.0f;

    // Flaga określająca, czy sortowanie ma być wykonywane tylko raz
    public bool runOnlyOnce = false;

    // Metoda wywoływana przy inicjalizacji skryptu
    private void Awake()
    {
        // Pobranie komponentu SpriteRenderer przypisanego do tego GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Metoda wywoływana na początku działania skryptu
    private void Start()
    {
        // Jeżeli flaga runOnlyOnce jest ustawiona, wykonaj sortowanie tylko raz
        if (runOnlyOnce)
        {
            UpdateSortingOrder();
        }
    }

    // Metoda wywoływana w każdej klatce
    private void Update()
    {
        // Jeżeli flaga runOnlyOnce nie jest ustawiona, wykonuj sortowanie w każdej klatce
        if (!runOnlyOnce)
        {
            UpdateSortingOrder();
        }
    }

    // Metoda aktualizująca wartość sortowania SpriteRenderer na podstawie pozycji y
    private void UpdateSortingOrder()
    {
        // Obliczenie wartości sortowania na podstawie pozycji y i offsetu yOffset
        spriteRenderer.sortingOrder = (int)((-(transform.position.y + yOffset) * 100) + 5000);
    }
}