using UnityEngine;
using UnityEngine.Serialization;

namespace Items.Interactable
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [FormerlySerializedAs("unlockableToOpen")] public Door doorToOpen;  // Upewnij się, że ten obiekt jest przypisany w inspektorze
        public Sprite leverActiveSprite;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact(GameObject interactor)
        {
            if (doorToOpen != null)
            {
                spriteRenderer.sprite = leverActiveSprite;
                doorToOpen.Unlock();
            }
            else
            {
                Debug.LogError("doorToOpen is not assigned in the inspector!");
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