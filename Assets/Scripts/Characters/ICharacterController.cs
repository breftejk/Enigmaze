using UnityEngine;

namespace Characters
{
    // Interfejs definiujący metodę do poruszania postacią
    public interface ICharacterController
    {
        // Metoda próbująca poruszyć postać w określonym kierunku
        bool TryMoving(Vector2 direction);
    }
}