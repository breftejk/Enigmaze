using UnityEngine;

public class Lever : MonoBehaviour
{
    public Door doorToOpen; // Referencja do drzwi, które mają być otwarte
    public Sprite leverActiveSprite; // Sprite aktywnej dźwigni

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        spriteRenderer.sprite = leverActiveSprite; // Zmień sprite dźwigni na aktywny
        doorToOpen.OpenDoor(); // Otwórz drzwi
    }
}