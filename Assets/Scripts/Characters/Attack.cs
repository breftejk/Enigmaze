using System.Collections;
using UnityEngine;
using System.Linq;

namespace Characters
{
    public class Attack : MonoBehaviour, IAttack
    {
        // Ilość obrażeń zadawanych przez atak
        public float attackDamage = 10f;

        // Czas trwania ataku
        public float attackDuration = 0.1f;

        // Czas odnowienia ataku
        public float attackCooldown = 3f;
        
        // Flaga określająca stan możliwości ataku
        private bool canAttack = true;

        // Referencja do zarządzania punktacją
        public UI.LevelScore levelScore;
        
        // Właściwość zwracająca komponent animatora
        private Animator animator => GetComponentInParent<Animator>();

        // Właściwość zwracająca komponent BoxCollider2D
        private BoxCollider2D swordBoxCollider => GetComponent<BoxCollider2D>();

        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            swordBoxCollider.enabled = false; // Wyłącz kolider na starcie
        }

        // Metoda wywoływana, gdy obiekt 2D wejdzie w kolizję z koliderem
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collider = collision.GetComponent<Collider2D>().gameObject;
            Debug.Log($"Collision detected between {gameObject.tag} and {collider.tag}");
            if (gameObject.CompareTag("EnemySword") && collider.CompareTag("Player"))
                DamageTarget(collision, "Enemy attacking player");
            else if (gameObject.CompareTag("PlayerSword") && collider.CompareTag("Enemy"))
            {
                DamageTarget(collision, "Player attacking enemy");
                levelScore.AddScore(10);
            }
        }

        // Metoda wywołująca atak
        public void TriggerAttack()
        {
            if (!animator.GetBool("IsAttacking") && canAttack) StartCoroutine(PerformAttack());
        }

        // Metoda odtwarzająca dźwięk ataku
        private void PlayAttackSound()
        {
            AudioSource audioSource = GetComponentsInParent<AudioSource>().FirstOrDefault();
            Debug.Log($"Audio source: {audioSource}");
            if (audioSource)
            {
                audioSource.Play();
            }
        }

        // Coroutine obsługująca logikę ataku
        private IEnumerator PerformAttack()
        {
            canAttack = false;
            swordBoxCollider.enabled = true; // Włącz kolider podczas ataku
            animator.SetBool("IsAttacking", true);
            try
            {
                PlayAttackSound();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error playing attack sound: {e.Message}");
            }

            yield return new WaitForSeconds(attackDuration);

            swordBoxCollider.enabled = false; // Wyłącz kolider po zakończeniu animacji ataku
            animator.SetBool("IsAttacking", false);

            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }

        // Metoda zadająca obrażenia celowi
        private void DamageTarget(Collider2D target, string message)
        {
            var targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
                Debug.Log($"{message}: {gameObject.name} inflicted {attackDamage} damage on {target.gameObject.name}");
            }
        }
    }
}