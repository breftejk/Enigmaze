using UnityEngine;
using Characters;

namespace Items.Interactable
{
    public class HealingItem : MonoBehaviour, IInteractable
    {
        // Ilość zdrowia przywracanego przez przedmiot
        public int healthAmount = 20;

        // Metoda wywoływana podczas interakcji z przedmiotem
        public void Interact(GameObject interactor)
        {
            var health = interactor.GetComponent<IHealth>();
            if (health != null)
            {
                health.Heal(healthAmount);  // Leczenie interaktora
                Destroy(gameObject);  // Zniszczenie przedmiotu po użyciu
            }
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z przedmiotem
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);  // Interakcja z przedmiotem
            }
        }
    }
}