using UnityEngine;

namespace Characters
{
    // Kontroler AI dla wrogów obsługujący ruch, animację oraz unikanie przeszkód.
    public class AIEnemyController : CharacterBaseController
    {
        // Cel, do którego AI ma dążyć (gracz)
        public Transform target;

        // Minimalna odległość do celu, przy której AI przestanie się poruszać
        public float stopDistance = 0.1f;

        // Odległość wykrywania przeszkód
        public float obstacleDetectionDistance = 1f;

        // Kąt odchylenia do sprawdzenia po bokach
        public float sideDetectionAngle = 45f;

        // Promień wykrywania gracza
        public float detectionRadius = 2f;

        // Metoda wywoływana co stałą liczbę klatek fizyki.
        // Obsługuje ruch AI, animację, unikanie przeszkód oraz wykrywanie gracza.
        private void FixedUpdate()
        {
            if (animator.GetBool("IsAttacking")) return;

            isWalking = false;

            if (target == null)
            {
                animator.SetBool("IsWalking", false);
                return;
            }

            var distanceToTarget = Vector2.Distance(target.position, transform.position);

            if (distanceToTarget <= stopDistance)
            {
                animator.SetBool("IsWalking", false);
                attackComponent.TriggerAttack();
                return;
            }

            if (distanceToTarget <= detectionRadius)
            {
                Vector2 direction = (target.position - transform.position).normalized;

                if (distanceToTarget > stopDistance)
                {
                    var avoidanceDirection = AvoidObstacles(direction);

                    var success = TryMoving(avoidanceDirection);

                    if (!success)
                    {
                        animator.SetBool("IsWalking", false);
                        return;
                    }

                    isWalking = true;
                    animator.SetBool("IsWalking", isWalking);

                    UpdateAnimation(avoidanceDirection);
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }

            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
                audioSource.mute = !animator.GetBool("IsWalking");
        }

        // Sprawdza obecność przeszkód w kierunku ruchu i zwraca skorygowany kierunek ruchu, aby ich uniknąć.
        // Parametr direction określa pierwotny kierunek ruchu.
        // Zwraca skorygowany kierunek ruchu, aby uniknąć przeszkód.
        private Vector2 AvoidObstacles(Vector2 direction)
        {
            var hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionDistance,
                contactFilter.layerMask);

            if (hit.collider != null)
            {
                Vector2 leftCheck = Quaternion.Euler(0, 0, sideDetectionAngle) * direction;
                Vector2 rightCheck = Quaternion.Euler(0, 0, -sideDetectionAngle) * direction;

                var leftHit = Physics2D.Raycast(transform.position, leftCheck, obstacleDetectionDistance,
                    contactFilter.layerMask);
                var rightHit = Physics2D.Raycast(transform.position, rightCheck, obstacleDetectionDistance,
                    contactFilter.layerMask);

                if (leftHit.collider == null) return leftCheck.normalized;

                if (rightHit.collider == null) return rightCheck.normalized;

                if (leftHit.distance > rightHit.distance)
                    return leftCheck.normalized;
                return rightCheck.normalized;
            }

            return direction;
        }

        // Aktualizuje animację postaci na podstawie kierunku ruchu.
        // Parametr direction określa kierunek ruchu.
        private void UpdateAnimation(Vector2 direction)
        {
            if (direction.y != 0)
            {
                if (direction.y < 0)
                    animator.SetInteger("Direction", 1); // Down
                else
                    animator.SetInteger("Direction", -1); // Up
            }
            else
            {
                animator.SetInteger("Direction", 0); // Horizontal
            }

            if (direction.x != 0) spriteRenderer.flipX = direction.x < 0;
        }
    }
}