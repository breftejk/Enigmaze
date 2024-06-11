using UnityEngine;
using UnityEngine.Serialization;

namespace Items.Interactable
{
    public class Lever : MonoBehaviour, IInteractable
    {
        // Referencja do drzwi, które mają zostać otwarte po użyciu dźwigni
        public Door doorToOpen;

        // Sprite aktywnej dźwigni
        public Sprite leverActiveSprite;

        // Referencja do komponentu SpriteRenderer
        private SpriteRenderer spriteRenderer;
        
        // Metoda wywoływana podczas interakcji z dźwignią
        public void Interact(GameObject interactor)
        {
            if (doorToOpen != null)
            {
                spriteRenderer.sprite = leverActiveSprite;  // Zmiana sprite dźwigni na aktywny
                doorToOpen.Unlock();  // Otworzenie drzwi
            }
            else
            {
                Debug.LogError("doorToOpen is not assigned in the inspector!");
            }
        }

        // Metoda wywoływana podczas inicjalizacji skryptu
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z dźwignią
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
    }
}