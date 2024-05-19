using System;
using UnityEngine;
using Characters;
using Unity.VisualScripting;

public class Door : MonoBehaviour
{
    public Sprite openDoorSprite; // Sprite otwartych drzwi

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Variables.Object(other).Get<Boolean>("HasKey"))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = openDoorSprite; // Zmień sprite na otwarte drzwi
        boxCollider.enabled = false; // Wyłącz kolider
    }
}