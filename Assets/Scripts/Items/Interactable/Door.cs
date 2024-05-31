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
    }
}