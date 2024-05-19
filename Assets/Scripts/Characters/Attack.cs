using System.Collections;
using UnityEngine;

namespace Characters
{
    public class Attack : MonoBehaviour
    {
        public Animator animator;
        public BoxCollider2D swordBoxCollider;
        public float attackDamage = 10f;
        public float attackDuration = 0.1f;
        public float attackCooldown = 3f;
        private bool _canAttack = true;
        private bool _isAttacking = false;

        private void Start()
        {
            animator = GetComponentInParent<Animator>();
            swordBoxCollider = GetComponent<BoxCollider2D>();
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
            if (!_isAttacking && _canAttack) StartCoroutine(PerformAttack());
        }

        private IEnumerator PerformAttack()
        {
            _canAttack = false;
            _isAttacking = true;
            swordBoxCollider.enabled = true; // Włącz kolider podczas ataku
            animator.SetBool("IsAttacking", true);

            yield return new WaitForSeconds(attackDuration);

            swordBoxCollider.enabled = false; // Wyłącz kolider po zakończeniu animacji ataku
            animator.SetBool("IsAttacking", false);
            _isAttacking = false;

            yield return new WaitForSeconds(attackCooldown);

            _canAttack = true;
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