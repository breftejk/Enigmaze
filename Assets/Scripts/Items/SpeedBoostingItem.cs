using UnityEngine;
using System.Collections;
using Characters;

public class SpeedBoostingItem : MonoBehaviour
{
    public float speedMultiplier = 2.0f; // Mnożnik prędkości
    public float duration = 5.0f; // Czas trwania efektu w sekundach

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplySpeedBoost(other));
        }
    }

    private IEnumerator ApplySpeedBoost(Collider2D player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            float originalSpeed = playerController.movementSpeed; // Zapisz oryginalną prędkość
            playerController.movementSpeed *= speedMultiplier; // Zwiększ prędkość

            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(duration); // Poczekaj określony czas

            playerController.movementSpeed = originalSpeed; // Przywróć oryginalną prędkość
            Destroy(gameObject); // Zniszcz obiekt booster po jego użyciu
        }
    }
}