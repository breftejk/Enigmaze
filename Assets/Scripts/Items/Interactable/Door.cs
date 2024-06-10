using Characters;
using Unity.VisualScripting;
using UnityEngine;

namespace Items.Interactable
{
    public class Door : MonoBehaviour, IUnlockable
    {
        public Sprite openDoorSprite;
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Unlock()
        {
            spriteRenderer.sprite = openDoorSprite;
            boxCollider.enabled = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact(other.gameObject);
            }
        }
        
        public void Interact(GameObject interactor)
        {
            var player = interactor.GetComponent<PlayerController>();
            if (player != null && Variables.Object(player).Get<bool>("HasKey"))
            {
                Unlock();
                Variables.Object(player).Set("HasKey", false);
            }
        }
    }
}