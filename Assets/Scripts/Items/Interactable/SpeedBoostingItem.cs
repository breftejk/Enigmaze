using UnityEngine;
using Characters;
using System.Collections;

namespace Items.Interactable
{
    public class SpeedBoostingItem : MonoBehaviour, IInteractable
    {
        // Współczynnik zwiększenia prędkości
        public float speedMultiplier = 2.0f;

        // Czas trwania zwiększenia prędkości
        public float duration = 1.0f;

        // Flaga zapobiegająca ponownemu użyciu przedmiotu
        private bool isUsed = false;

        // Referencja do komponentu SpriteRenderer
        private SpriteRenderer spriteRenderer;

        // Referencja do komponentu Collider2D
        private Collider2D collider;

        // Metoda wywoływana podczas interakcji z przedmiotem
        public void Interact(GameObject interactor)
        {
            if (isUsed) return; // Jeśli przedmiot już użyty, wyjdź z metody

            var player = interactor.GetComponent<PlayerController>();
            if (player != null)
            {
                isUsed = true; // Oznacz przedmiot jako użyty
                StartCoroutine(ApplySpeedBoost(player));
            }
        }
        
        // Metoda wywoływana podczas inicjalizacji skryptu
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
        }

        // Coroutine stosująca zwiększenie prędkości
        private IEnumerator ApplySpeedBoost(PlayerController player)
        {
            float originalSpeed = player.movementSpeed; // Zapisz oryginalną prędkość
            player.movementSpeed *= speedMultiplier; // Zwiększ prędkość

            // Ukryj obiekt i wyłącz kolizje
            spriteRenderer.enabled = false;
            collider.enabled = false;

            yield return new WaitForSeconds(duration); // Poczekaj określony czas

            player.movementSpeed = originalSpeed; // Przywróć oryginalną prędkość
            // Nie niszcz obiektu, ale pozostaw go ukryty i nieaktywny
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z przedmiotem
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
    }
}