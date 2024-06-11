using UnityEngine;

namespace Items.Interactable
{
    // Interfejs definiujący elementy, które można odblokować
    public interface IUnlockable
    {
        // Metoda do odblokowania elementu
        void Unlock();
    }
}