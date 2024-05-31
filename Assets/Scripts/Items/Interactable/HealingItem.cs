using UnityEngine;
using Characters;

namespace Items.Interactable
{
    public class HealingItem : MonoBehaviour, IInteractable
    {
        public int healthAmount = 20;

        public void Interact(GameObject interactor)
        {
            var health = interactor.GetComponent<IHealth>();
            if (health != null)
            {
                health.Heal(healthAmount);
                Destroy(gameObject);
            }
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