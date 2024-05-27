using System.Collections;
using UnityEngine;
using Characters;

public class Trap : MonoBehaviour
{
    public float damagePerSecond = 5f; // Obrażenia zadawane co sekundę

    
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DamageOverTime(other));
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines(); // Zatrzymaj zadawanie obrażeń, gdy gracz opuści pułapkę
        }
    }

    private IEnumerator DamageOverTime(Collider2D player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            while (true)
            {
                playerHealth.TakeDamage(damagePerSecond);
                Debug.Log($"Trap damaged {player.name}: -{damagePerSecond} HP");
                yield return new WaitForSeconds(1); // Czekaj sekundę przed zadaniem kolejnych obrażeń
            }
        }
    }
}