using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Characters
{
    public class Health : MonoBehaviour, IHealth
    {
        public float maxHealth = 100f;
        public float currentHealth;

        public Animator animator => GetComponent<Animator>();

        public Slider healthSlider;
        public GameObject healthBarUIPrefab;
        public Vector3 healthBarOffset = new(0, 1.5f, 0); // Offset to position the health bar above the enemy

        public GameObject deathUI;
        
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
                // Instantiate health bar above enemy
                var healthBarUI = Instantiate(healthBarUIPrefab, transform.position + healthBarOffset,
                    Quaternion.identity, transform);
                healthSlider = healthBarUI.GetComponentInChildren<Slider>();
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;

                // Set the target for the HealthBarFollow script
                var healthBarFollow = healthBarUI.GetComponent<HealthBarFollow>();
                healthBarFollow.target = transform;
                healthBarFollow.offset = healthBarOffset;
            }
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthSlider != null) healthSlider.value = currentHealth;

            if (currentHealth <= 0) Die();
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthSlider != null) healthSlider.value = currentHealth;
        }

        private void Die()
        {
            if (animator.GetBool("IsDead")) return; // Jeśli postać już "umarła", nie wykonuj tej metody ponownie.

            animator.SetBool("IsDead", true);

            if (gameObject.tag == "Player")
            {
                GetComponent<PlayerInput>().enabled = false;
            } else if (gameObject.tag == "Enemy")
            {
                GetComponent<AIEnemyController>().enabled = false;
                GetComponentInChildren<Canvas>().enabled = false;
            }
            
            GetComponent<BoxCollider2D>().enabled = false;

            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
                if (audioSource.name == "DeathSound")
                    audioSource.Play();
                else audioSource.mute = true;

            StartCoroutine(DeathBehaviourAfterDelay(2f, gameObject));
        }

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