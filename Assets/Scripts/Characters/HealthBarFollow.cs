using UnityEngine;

namespace Characters
{
    public class HealthBarFollow : MonoBehaviour
    {
        // Cel (wróg), który pasek zdrowia ma śledzić
        public Transform target;

        // Przesunięcie od pozycji celu
        public Vector3 offset;

        // Metoda wywoływana w każdej klatce po zakończeniu aktualizacji wszystkich obiektów
        private void LateUpdate()
        {
            // Jeśli cel jest ustawiony, aktualizuj pozycję paska zdrowia
            if (target != null) 
                transform.position = target.position + offset;
        }
    }
}