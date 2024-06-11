using UnityEngine;
using Characters;
using Unity.VisualScripting;

namespace Items.Interactable
{
    public class Key : MonoBehaviour, IInteractable
    {
        // Metoda wywoływana podczas interakcji z kluczem
        public void Interact(GameObject interactor)
        {
            var player = interactor.GetComponent<PlayerController>();
            if (player != null)
            {
                // Ustawienie zmiennej "HasKey" na true dla gracza
                Variables.Object(player).Set("HasKey", true);

                // Zniszczenie obiektu klucza po interakcji
                Destroy(gameObject);
            }
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z kluczem
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
    }
}