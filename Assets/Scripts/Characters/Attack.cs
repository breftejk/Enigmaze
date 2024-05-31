using System.Collections;
using UnityEngine;
using System.Linq;

namespace Characters
{
    public class Attack : MonoBehaviour, IAttack
    {
        private Animator animator =>  GetComponentInParent<Animator>();
        private BoxCollider2D swordBoxCollider => GetComponent<BoxCollider2D>();
        public float attackDamage = 10f;
        public float attackDuration = 0.1f;
        public float attackCooldown = 3f;
        private bool canAttack = true;
        private bool isAttacking = false;

        private void Start()
        {
            swordBoxCollider.enabled = false; // Wyłącz kolider na starcie
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collider = collision.GetComponent<Collider2D>().gameObject;
            Debug.Log($"Collision detected between {gameObject.tag} and {collider.tag}");
            if (gameObject.CompareTag("EnemySword") && collider.CompareTag("Player"))
                DamageTarget(collision, "Enemy attacking player");
            else if (gameObject.CompareTag("PlayerSword") && collider.CompareTag("Enemy"))
                DamageTarget(collision, "Player attacking enemy");
        }

        public void TriggerAttack()
        {
            if (!isAttacking && canAttack) StartCoroutine(PerformAttack());
        }

        private void PlayAttackSound()
        {
            AudioSource audioSource = GetComponentsInParent<AudioSource>().FirstOrDefault();
            Debug.Log($"Audio source: {audioSource}");
            if (audioSource)
            {
                audioSource.Play();
            }
        }

        private IEnumerator PerformAttack()
        {
            canAttack = false;
            isAttacking = true;
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
            isAttacking = false;

            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }

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