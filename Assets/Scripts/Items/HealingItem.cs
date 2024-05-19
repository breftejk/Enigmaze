using UnityEngine;
using Characters;

public class HealingItem : MonoBehaviour
{
    public int healthAmount = 20; // Ilość zdrowia, które zostanie przywrócone

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                Destroy(gameObject); // Zniszcz obiekt zdrowia po jego użyciu
            }
        }
    }
}