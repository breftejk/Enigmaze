using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;

        public Animator animator;

        public Slider healthSlider;
        public GameObject healthBarUIPrefab;
        public Vector3 healthBarOffset = new(0, 1.5f, 0); // Offset to position the health bar above the enemy

        private void Start()
        {
            animator = GetComponent<Animator>();
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
            GetComponent<AIEnemyController>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;


            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
                if (audioSource.name == "DeathSound")
                    audioSource.Play();
                else audioSource.mute = true;

            StartCoroutine(RemoveCharacterAfterDelay(6f));
        }

        private IEnumerator RemoveCharacterAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}