using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Singleton, aby łatwo uzyskać dostęp do menedżera z innych skryptów

    private int score = 0;  // Prywatna zmienna przechowująca wynik

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);  // Opcjonalnie: zapobiegaj zniszczeniu tego obiektu przy zmianie sceny
        LoadScore();
    }

    public void AddScore(int points)
    {
        score += points;  // Dodaj punkty do wyniku
        SaveScore();
    }

    public void ResetScore()
    {
        score = 0;  // Zresetuj wynik
        SaveScore();
    }

    public int GetScore()
    {
        return score;
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
            score = PlayerPrefs.GetInt("Score");
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}