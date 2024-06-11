using UnityEngine;

public class Level_Move : MonoBehaviour
{
    // Referencja do poziomu punktacji UI
    public UI.LevelScore levelScore;
    
    // Numer aktualnego poziomu
    public int levelNumber;

    // Metoda wywoływana, gdy inny obiekt wejdzie w kolizję z tym obiektem 2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy tag obiektu kolidującego to "Player"
        if (other.tag == "Player")
        {
            // Wyłączanie komponentu Collider2D, aby zapobiec ponownemu wywołaniu kolizji
            GetComponent<Collider2D>().enabled = false;

            // Dodanie punktów do wyniku gracza
            ScoreManager.Instance.AddScore(levelScore.GetLevelScore());

            // Ustawienie numeru następnego poziomu
            GameManager.Instance.SetLevel(levelNumber + 1);

            // Zapis postępów gry
            GameManager.Instance.SaveProgress();

            // Wczytanie sceny następnego poziomu
            GameManager.Instance.LoadScene($"Level_{levelNumber + 1}");
        }
    }
}