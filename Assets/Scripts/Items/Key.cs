using UnityEngine;
using Characters;
using Unity.VisualScripting;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Variables.Object(other).Set("HasKey", true); // Ustaw, że gracz ma klucz
            Destroy(gameObject); // Zniszcz obiekt klucza
        }
    }
}