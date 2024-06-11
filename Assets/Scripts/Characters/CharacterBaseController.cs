using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterBaseController : MonoBehaviour
    {
        // Szybkość poruszania się postaci
        public float movementSpeed = 1f;

        // Filtr kontaktów używany do wykrywania kolizji
        public ContactFilter2D contactFilter;
        
        // Komponent ataku
        private protected Attack attackComponent => GetComponentInChildren<Attack>();

        // Komponent zdrowia
        private protected IHealth health => GetComponent<IHealth>();

        // Komponent animatora
        private protected Animator animator => GetComponent<Animator>();

        // Flaga określająca, czy postać jest w ruchu
        private protected bool isWalking = false;

        // Komponent SpriteRenderer
        private protected SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
        
        // Komponent Rigidbody2D
        private Rigidbody2D rb => GetComponent<Rigidbody2D>();

        // Lista przechowująca wyniki kolizji
        private readonly List<RaycastHit2D> _collisions = new();

        // Próbuje poruszyć postać w określonym kierunku
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

        // Porusza postać w określonym kierunku
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