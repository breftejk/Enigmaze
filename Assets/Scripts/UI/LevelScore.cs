using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelScore : MonoBehaviour
    {
        public TMP_Text scoreText;  // Referencja do komponentu tekstowego UI, gdzie wynik będzie wyświetlany
        private int levelScore = 0;  // Prywatna zmienna przechowująca wynik
        
        public void AddScore(int score)
        {
            levelScore += score;
            if (scoreText != null)
                scoreText.text = "Score: " + levelScore;  // Aktualizacja tekstu wyświetlanego na UI
        }

        public int GetLevelScore()
        {
            return levelScore;
        }
    }
}