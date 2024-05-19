using UnityEngine;

namespace Characters
{
    public class HealthBarFollow : MonoBehaviour
    {
        public Transform target; // The target (enemy) to follow
        public Vector3 offset; // Offset from the target's position

        private void LateUpdate()
        {
            if (target != null) transform.position = target.position + offset;
        }
    }
}