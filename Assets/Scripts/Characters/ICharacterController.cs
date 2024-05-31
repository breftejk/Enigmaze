using UnityEngine;

namespace Characters
{
    public interface ICharacterController
    {
        bool TryMoving(Vector2 direction);
    }
}