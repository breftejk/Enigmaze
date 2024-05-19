using UnityEngine;

namespace Characters
{
    /// <summary>
    ///     Kontroler AI dla wrogów obsługujący ruch, animację oraz unikanie przeszkód.
    /// </summary>
    public class AIEnemyController : CharacterBaseController
    {
        /// <summary>
        ///     Cel, do którego AI ma dążyć (gracz).
        /// </summary>
        public Transform target;

        /// <summary>
        ///     Minimalna odległość do celu, przy której AI przestanie się poruszać.
        /// </summary>
        public float stopDistance = 0.1f;

        /// <summary>
        ///     Odległość wykrywania przeszkód.
        /// </summary>
        public float obstacleDetectionDistance = 1f;

        /// <summary>
        ///     Kąt odchylenia do sprawdzenia po bokach.
        /// </summary>
        public float sideDetectionAngle = 45f;

        /// <summary>
        ///     Promień wykrywania gracza.
        /// </summary>
        public float detectionRadius = 5f;

        public Attack attackComponent;

        private void Awake()
        {
            attackComponent = GetComponentInChildren<Attack>();
        }

        /// <summary>
        ///     Metoda wywoływana co stałą liczbę klatek fizyki.
        ///     Obsługuje ruch AI, animację, unikanie przeszkód oraz wykrywanie gracza.
        /// </summary>
        private void FixedUpdate()
        {
            if (animator.GetBool("IsAttacking")) return;

            _isWalking = false;

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

                    _isWalking = true;
                    animator.SetBool("IsWalking", _isWalking);

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

        /// <summary>
        ///     Sprawdza obecność przeszkód w kierunku ruchu i zwraca skorygowany kierunek ruchu, aby ich uniknąć.
        /// </summary>
        /// <param name="direction">Pierwotny kierunek ruchu.</param>
        /// <returns>Skorygowany kierunek ruchu, aby uniknąć przeszkód.</returns>
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

        /// <summary>
        ///     Aktualizuje animację postaci na podstawie kierunku ruchu.
        /// </summary>
        /// <param name="direction">Kierunek ruchu.</param>
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

            if (direction.x != 0) _spriteRenderer.flipX = direction.x < 0;
        }
    }
}