using UnityEngine;

namespace Items.Interactable
{
    // Interfejs definiujący elementy, z którymi można wchodzić w interakcję
    public interface IInteractable
    {
        // Metoda do obsługi interakcji z elementem
        void Interact(GameObject interactor);
    }
}