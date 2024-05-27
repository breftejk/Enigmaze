using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters
{
    /// <summary>
    ///     Kontroler gracza obsługujący ruch gracza i animację.
    /// </summary>
    public class PlayerController : CharacterBaseController
    {
        public Attack attackComponent;
        private Vector2 movementInput = Vector2.zero;

        private void Awake()
        {
            attackComponent = GetComponentInChildren<Attack>();
        }

        /// <summary>
        ///     Metoda wywoływana co stałą liczbę klatek fizyki.
        ///     Obsługuje ruch gracza i animację.
        /// </summary>
        private void FixedUpdate()
        {
            if (animator.GetBool("IsAttacking")) return;
            _isWalking = false;

            var success = TryMoving(movementInput);

            if (!success)
            {
                animator.SetBool("IsWalking", false);
                return;
            }

            _isWalking = true;
            animator.SetBool("IsWalking", _isWalking);

            if (movementInput.x == 0 && movementInput.y != 0)
            {
                if (movementInput.y < 0)
                    animator.SetInteger("Direction", 1);
                else
                    animator.SetInteger("Direction", -1);
            }
            else
            {
                if (movementInput.x < 0)
                    _spriteRenderer.flipX = true;
                else if (movementInput.x > 0) _spriteRenderer.flipX = false;

                animator.SetInteger("Direction", 0);
            }
        }

        /// <summary>
        ///     Metoda obsługująca ruch gracza na podstawie wejścia.
        /// </summary>
        /// <param name="movementValue">Wartość wejścia ruchu gracza.</param>
        private void OnMove(InputValue movementValue)
        {
            movementInput = movementValue.Get<Vector2>();
        }

        public void OnFire()
        {
            if (animator.GetBool("IsAttacking")) return;
            animator.SetBool("IsWalking", false);
            attackComponent.TriggerAttack();
        }
    }
}