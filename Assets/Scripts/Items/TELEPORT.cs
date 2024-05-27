using System.Collections;
using UnityEngine;

public class TELEPORT : MonoBehaviour
{
    public GameObject targetBlock;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangePosition(other));
        }
    }

    private IEnumerator ChangePosition(Collider2D other)
    {
        if (targetBlock != null)
        {
            other.transform.position = targetBlock.transform.position;
            Debug.Log($"Player {other.name} teleported to {targetBlock.name} at {targetBlock.transform.position}");
        }
        else
        {
            Debug.LogError("Target block is not set for the teleport.");
        }
        yield return null;

        
    }
}
