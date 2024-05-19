using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterBaseController : MonoBehaviour
    {
        /// <summary>
        ///     Szybkość poruszania się gracza.
        /// </summary>
        public float movementSpeed = 1f;

        /// <summary>
        ///     Filtr kontaktów używany do wykrywania kolizji.
        /// </summary>
        public ContactFilter2D contactFilter;

        private protected Health _health;

        private protected Animator animator;
        private readonly List<RaycastHit2D> _collisions = new();

        private bool _isAttacking = false;

        private protected bool _isWalking; // Zmienna do śledzenia stanu ruchu
        private Rigidbody2D _rb;
        private protected SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _health = GetComponent<Health>();
        }

        /// <summary>
        ///     Próbuje poruszyć gracza w określonym kierunku.
        /// </summary>
        /// <param name="direction">Kierunek, w którym ma zostać przesunięty gracz.</param>
        /// <returns>Prawda, jeśli gracz może zostać przesunięty w danym kierunku; fałsz w przeciwnym razie.</returns>
        private protected bool TryMoving(Vector2 direction)
        {
            if (direction == Vector2.zero) return false;
            var count = _rb.Cast(direction, contactFilter, _collisions, movementSpeed * Time.fixedDeltaTime);

            if (count == 0)
            {
                _rb.MovePosition(_rb.position + movementSpeed * Time.fixedDeltaTime * direction);
                return true;
            }

            return false;
        }
    }
}