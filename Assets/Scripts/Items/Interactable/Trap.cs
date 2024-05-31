using System.Collections;
using UnityEngine;
using Characters;

namespace Items.Interactable
{
    public class Trap : MonoBehaviour, IInteractable
    {
        public float damagePerSecond = 5f;

        public void Interact(GameObject interactor)
        {
            var health = interactor.GetComponent<IHealth>();
            if (health != null)
            {
                StartCoroutine(DamageOverTime(health));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }

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