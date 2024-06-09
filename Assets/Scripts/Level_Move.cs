using UnityEngine;

public class Level_Move : MonoBehaviour
{
    public UI.LevelScore levelScore;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            ScoreManager.Instance.AddScore(levelScore.GetLevelScore());
            GameManager.Instance.IncrementLevel();
        }
    }
}