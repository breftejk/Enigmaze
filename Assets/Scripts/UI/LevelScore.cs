using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelScore : MonoBehaviour
    {
        // Referencja do komponentu tekstowego UI, gdzie wynik będzie wyświetlany
        public TMP_Text scoreText;

        // Prywatna zmienna przechowująca wynik
        private int levelScore = 0;

        // Metoda dodająca punkty do wyniku poziomu i aktualizująca tekst wyświetlany na UI
        public void AddScore(int score)
        {
            levelScore += score;
            if (scoreText != null)
                scoreText.text = "Score: " + levelScore;  // Aktualizacja tekstu wyświetlanego na UI
        }

        // Metoda zwracająca aktualny wynik poziomu
        public int GetLevelScore()
        {
            return levelScore;
        }
    }
}