using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Singleton, aby łatwo uzyskać dostęp do menedżera z innych skryptów
    public static ScoreManager Instance;

    // Prywatna zmienna przechowująca wynik
    private int score = 0;

    // Metoda do dodawania punktów do wyniku
    public void AddScore(int points)
    {
        score += points;  // Dodaj punkty do wyniku
        SaveScore();  // Zapisz wynik
    }

    // Metoda do resetowania wyniku
    public void ResetScore()
    {
        score = 0;  // Zresetuj wynik
        SaveScore();  // Zapisz wynik
    }

    // Metoda do pobierania bieżącego wyniku
    public int GetScore()
    {
        return score;
    }
    
    // Metoda wywoływana podczas inicjalizacji skryptu
    private void Awake()
    {
        // Inicjalizacja singletonu
        Instance = this;

        // Opcjonalnie: zapobiegaj zniszczeniu tego obiektu przy zmianie sceny
        DontDestroyOnLoad(gameObject);

        // Wczytaj wynik z PlayerPrefs
        LoadScore();
    }

    // Prywatna metoda do wczytywania wyniku z PlayerPrefs
    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
    }

    // Prywatna metoda do zapisywania wyniku do PlayerPrefs
    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}