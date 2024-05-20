using UnityEngine;
using UnityEngine.UI;  // Pamiętaj, aby dodać tę przestrzeń nazw, jeśli będziesz używać komponentów UI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Singleton, aby łatwo uzyskać dostęp do menedżera z innych skryptów

    private int score = 0;  // Prywatna zmienna przechowująca wynik
    public Text scoreText;  // Referencja do komponentu tekstowego UI, gdzie wynik będzie wyświetlany

    void Awake()
    {
        // Implementacja singletona
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Opcjonalnie: zapobiegaj zniszczeniu tego obiektu przy zmianie sceny
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;  // Dodaj punkty do wyniku
        UpdateScoreUI();  // Aktualizuj interfejs użytkownika
    }

    public void ResetScore()
    {
        score = 0;  // Zresetuj wynik
        UpdateScoreUI();  // Aktualizuj interfejs użytkownika
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;  // Aktualizacja tekstu wyświetlanego na UI
    }
}