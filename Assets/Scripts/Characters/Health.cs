using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Characters
{
    public class Health : MonoBehaviour, IHealth
    {
        // Maksymalna ilość zdrowia
        public float maxHealth = 100f;

        // Bieżąca ilość zdrowia
        public float currentHealth;

        // Suwak wyświetlający pasek zdrowia
        public Slider healthSlider;

        // Prefabrykat paska zdrowia
        public GameObject healthBarUIPrefab;

        // Przesunięcie do umieszczenia paska zdrowia nad wrogiem
        public Vector3 healthBarOffset = new(0, 1.5f, 0);

        // UI wyświetlane po śmierci
        public GameObject deathUI;
        
        // Animator do zarządzania animacjami
        private Animator animator => GetComponent<Animator>();

        // Metoda wywoływana, gdy postać otrzymuje obrażenia
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthSlider != null) healthSlider.value = currentHealth;

            if (currentHealth <= 0) Die();
        }

        // Metoda wywoływana, gdy postać zostaje uleczona
        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthSlider != null) healthSlider.value = currentHealth;
        }
        
        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            currentHealth = maxHealth;

            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                // Tworzenie paska zdrowia nad wrogiem
                var healthBarUI = Instantiate(healthBarUIPrefab, transform.position + healthBarOffset,
                    Quaternion.identity, transform);
                healthSlider = healthBarUI.GetComponentInChildren<Slider>();
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;

                // Ustawienie celu dla skryptu HealthBarFollow
                var healthBarFollow = healthBarUI.GetComponent<HealthBarFollow>();
                healthBarFollow.target = transform;
                healthBarFollow.offset = healthBarOffset;
            }
        }

        // Metoda wywoływana, gdy postać umiera
        private void Die()
        {
            if (animator.GetBool("IsDead")) return; // Jeśli postać już "umarła", nie wykonuj tej metody ponownie.

            animator.SetBool("IsDead", true);

            if (gameObject.tag == "Player")
            {
                GetComponent<PlayerInput>().enabled = false;
            } 
            else if (gameObject.tag == "Enemy")
            {
                GetComponent<AIEnemyController>().enabled = false;
                GetComponentInChildren<Canvas>().enabled = false;
            }
            
            GetComponent<BoxCollider2D>().enabled = false;

            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
                if (audioSource.name == "DeathSound")
                    audioSource.Play();
                else 
                    audioSource.mute = true;

            StartCoroutine(DeathBehaviourAfterDelay(2f, gameObject));
        }

        // Coroutine obsługująca zachowanie po śmierci z opóźnieniem
        private IEnumerator DeathBehaviourAfterDelay(float delay, GameObject gameObject)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
            if (gameObject.tag == "Player")
            {
                deathUI.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
        }
    }
}