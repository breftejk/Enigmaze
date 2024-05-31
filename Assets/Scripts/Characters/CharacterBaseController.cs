using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterBaseController : MonoBehaviour
    {
        /// <summary>
        /// Szybkość poruszania się gracza.
        /// </summary>
        public float movementSpeed = 1f;
        
        private protected Attack attackComponent => GetComponentInChildren<Attack>();

        /// <summary>
        /// Filtr kontaktów używany do wykrywania kolizji.
        /// </summary>
        public ContactFilter2D contactFilter;

        private protected IHealth health => GetComponent<IHealth>();
        private protected Animator animator => GetComponent<Animator>();
        private protected bool isWalking = false;
        private protected SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
        
        private Rigidbody2D rb => GetComponent<Rigidbody2D>();

        private readonly List<RaycastHit2D> _collisions = new();

        /// <summary>
        /// Próbuje poruszyć gracza w określonym kierunku.
        /// </summary>
        /// <param name="direction">Kierunek, w którym ma zostać przesunięty gracz.</param>
        /// <returns>Prawda, jeśli gracz może zostać przesunięty w danym kierunku; fałsz w przeciwnym razie.</returns>
        public bool TryMoving(Vector2 direction)
        {
            if (direction == Vector2.zero) return false;

            // Próbuj poruszać się w pełnym kierunku
            if (MoveInDirection(direction))
            {
                return true;
            }

            // Jeśli pełny kierunek jest zablokowany, spróbuj poruszać się tylko w kierunku poziomym
            if (MoveInDirection(new Vector2(direction.x, 0)))
            {
                return true;
            }

            // Jeśli kierunek poziomy jest zablokowany, spróbuj poruszać się tylko w kierunku pionowym
            if (MoveInDirection(new Vector2(0, direction.y)))
            {
                return true;
            }

            // Jeśli wszystko zawiedzie, ruch nie jest możliwy
            return false;
        }

        /// <summary>
        /// Porusza gracza w określonym kierunku.
        /// </summary>
        /// <param name="direction">Kierunek, w którym ma zostać przesunięty gracz.</param>
        /// <returns>Prawda, jeśli gracz może zostać przesunięty w danym kierunku; fałsz w przeciwnym razie.</returns>
        private bool MoveInDirection(Vector2 direction)
        {
            var count = rb.Cast(direction, contactFilter, _collisions, movementSpeed * Time.fixedDeltaTime);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * direction);
                return true;
            }

            return false;
        }
    }
}