using UnityEngine;
using Characters;
using Unity.VisualScripting;

namespace Items.Interactable
{
    public class Key : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject interactor)
        {
            var player = interactor.GetComponent<PlayerController>();
            if (player != null)
            {
                Variables.Object(player).Set("HasKey", true);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
    }
}