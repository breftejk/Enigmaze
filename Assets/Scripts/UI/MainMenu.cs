using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        // Przycisk do wznowienia gry
        public Button resumeButton;

        // Przycisk do resetowania gry
        public Button resetButton;

        // Przycisk do wyjścia z gry
        public Button exitButton;

        // Tekst wyświetlający nazwę użytkownika
        public TMP_Text usernameText;

        // Tekst wyświetlający poziom
        public TMP_Text levelText;

        // Tekst wyświetlający wynik punktowy
        public TMP_Text scoreText;
        
        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            // Sprawdzenie, czy nazwa użytkownika jest ustawiona
            if (string.IsNullOrEmpty(GameManager.Instance.Username))
            {
                // Jeśli nazwa użytkownika nie jest ustawiona, załaduj scenę początkową
                GameManager.Instance.LoadScene("Start_Scene");
                return;
            }

            // Aktualizacja interfejsu użytkownika
            UpdateUI();

            // Dodanie słuchaczy zdarzeń do przycisków
            resumeButton.onClick.AddListener(ResumeGame);
            resetButton.onClick.AddListener(ResetGame);
            exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
        }

        // Metoda aktualizująca interfejs użytkownika
        private void UpdateUI()
        {
            // Sprawdzenie, czy referencje do tekstów nie są null
            if(usernameText == null || levelText == null)
                return;
            
            // Ustawienie tekstu z nazwą użytkownika
            usernameText.text = GameManager.Instance.Username;

            // Ustawienie tekstu z aktualnym poziomem
            levelText.text = $"Level: {GameManager.Instance.CurrentLevel}";

            // Ustawienie tekstu z wynikiem punktowym
            scoreText.text = $"Score: {ScoreManager.Instance.GetScore()}";
        }

        // Metoda wywoływana po kliknięciu przycisku wznowienia gry
        private void ResumeGame()
        {
            // Wczytanie bieżącej sceny gry
            GameManager.Instance.LoadScene();
        }

        // Metoda wywoływana po kliknięciu przycisku resetowania gry
        private void ResetGame()
        {
            // Resetowanie postępów gry
            GameManager.Instance.ResetProgress();

            // Resetowanie wyniku punktowego
            ScoreManager.Instance.ResetScore();

            // Wczytanie sceny początkowej
            GameManager.Instance.LoadScene("Start_Scene");
        }
    }
}