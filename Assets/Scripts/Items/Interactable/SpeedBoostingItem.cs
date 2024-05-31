using UnityEngine;
using Characters;
using System.Collections;

namespace Items.Interactable
{
    public class SpeedBoostingItem : MonoBehaviour, IInteractable
    {
        public float speedMultiplier = 2.0f;
        public float duration = 1.0f;
        private bool isUsed = false; // Flaga zapobiegająca ponownemu użyciu

        private SpriteRenderer spriteRenderer;
        private Collider2D collider;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
        }

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
    }
}