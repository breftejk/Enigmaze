using System.Collections;
using UnityEngine;
using Characters;

namespace Items.Interactable
{
    public class Trap : MonoBehaviour, IInteractable
    {
        // Ilość obrażeń zadawanych na sekundę
        public float damagePerSecond = 5f;

        // Metoda wywoływana podczas interakcji z pułapką
        public void Interact(GameObject interactor)
        {
            var health = interactor.GetComponent<IHealth>();
            if (health != null)
            {
                StartCoroutine(DamageOverTime(health));
            }
        }

        // Metoda wywoływana, gdy inny obiekt 2D wejdzie w kolizję z pułapką
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }

        // Metoda wywoływana, gdy inny obiekt 2D wyjdzie z kolizji z pułapką
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }

        // Coroutine zadająca obrażenia co sekundę
        private IEnumerator DamageOverTime(IHealth health)
        {
            while (true)
            {
                health.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1);
            }
        }
    }
}