using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters
{
    public class PlayerController : CharacterBaseController
    {
        // Wartość wejścia ruchu gracza
        private Vector2 movementInput = Vector2.zero;
        
        // Metoda obsługująca akcję strzelania.
        public void OnFire()
        {
            // Sprawdź, czy gracz nie atakuje
            if (animator.GetBool("IsAttacking")) return;
            animator.SetBool("IsWalking", false);
            attackComponent.TriggerAttack();
        }

        // Metoda wywoływana co stałą liczbę klatek fizyki.
        // Obsługuje ruch gracza i animację.
        private void FixedUpdate()
        {
            // Sprawdź, czy gracz nie atakuje
            if (animator.GetBool("IsAttacking")) return;
            isWalking = false;

            // Próbuj poruszać gracza
            var success = TryMoving(movementInput);

            // Jeśli ruch się nie powiódł, zatrzymaj animację chodzenia
            if (!success)
            {
                animator.SetBool("IsWalking", false);
                return;
            }

            // Aktualizuj stan chodzenia i animację
            isWalking = true;
            animator.SetBool("IsWalking", isWalking);

            UpdateAnimation();
        }

        // Metoda obsługująca ruch gracza na podstawie wejścia.
        // Parametr movementValue zawiera wartość wejścia ruchu gracza.
        private void OnMove(InputValue movementValue)
        {
            movementInput = movementValue.Get<Vector2>();
        }

        // Metoda aktualizująca animację gracza na podstawie kierunku ruchu.
        private void UpdateAnimation()
        {
            if (movementInput.x == 0 && movementInput.y != 0)
            {
                // Aktualizuj kierunek animacji na pionowy
                if (movementInput.y < 0)
                    animator.SetInteger("Direction", 1);
                else
                    animator.SetInteger("Direction", -1);
            }
            else
            {
                // Aktualizuj kierunek animacji na poziomy i flipowanie sprite'a
                if (movementInput.x < 0)
                    spriteRenderer.flipX = true;
                else if (movementInput.x > 0)
                    spriteRenderer.flipX = false;

                animator.SetInteger("Direction", 0);
            }
        }
    }
}