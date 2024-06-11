using Characters;
using Unity.VisualScripting;
using UnityEngine;

namespace Items.Interactable
{
    public class Door : MonoBehaviour, IUnlockable
    {
        // Sprite otwartych drzwi
        public Sprite openDoorSprite;

        // Referencja do komponentu SpriteRenderer
        private SpriteRenderer spriteRenderer;

        // Referencja do komponentu BoxCollider2D
        private BoxCollider2D boxCollider;
        
        // Metoda do interakcji z drzwiami
        public void Interact(GameObject interactor)
        {
            var player = interactor.GetComponent<PlayerController>();
            if (player != null && Variables.Object(player).Get<bool>("HasKey"))
            {
                Unlock();  // Odblokowanie drzwi
                Variables.Object(player).Set("HasKey", false);  // Ustawienie zmiennej "HasKey" na false po użyciu klucza
            }
        }

        // Metoda do odblokowania drzwi
        public void Unlock()
        {
            spriteRenderer.sprite = openDoorSprite;  // Zmiana sprite'a na otwarte drzwi
            boxCollider.enabled = false;  // Wyłączenie kolizji drzwi
        }

        // Metoda wywoływana podczas inicjalizacji skryptu
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z drzwiami
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);  // Interakcja z drzwiami
            }
        }
    }
}