using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PausedGame : MonoBehaviour
    {
        // Panel zawierający elementy UI do wyświetlenia po wciśnięciu ESC
        public GameObject pausedPanel;

        // Przycisk "Resume"
        public Button resumeButton;

        // Przycisk "Menu"
        public Button menuButton;

        // Przycisk "Quit"
        public Button quitButton;

        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            // Ukrycie panelu pauzy na początku
            if (pausedPanel != null)
                pausedPanel.SetActive(false);

            // Dodanie słuchaczy zdarzeń do przycisków
            resumeButton.onClick.AddListener(ResumeGame);
            menuButton.onClick.AddListener(GoToMenu);
            quitButton.onClick.AddListener(QuitGame);
        }

        // Metoda wywoływana w każdej klatce
        private void Update()
        {
            // Nasłuchiwanie wciśnięcia klawisza ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Pokaż panel pauzy po wciśnięciu ESC
                if (pausedPanel != null)
                    pausedPanel.SetActive(true);

                // Pauza dźwięku
                AudioListener.pause = true;

                // Zatrzymanie czasu gry
                Time.timeScale = 0f;
            }
        }

        // Obsługa kliknięcia przycisku "Resume"
        private void ResumeGame()
        {
            // Ukrycie panelu pauzy i wznowienie gry
            if (pausedPanel != null)
                pausedPanel.SetActive(false);

            // Wznowienie dźwięku
            AudioListener.pause = false;

            // Przywrócenie czasu gry
            Time.timeScale = 1f;
        }

        // Obsługa kliknięcia przycisku "Menu"
        private void GoToMenu()
        {
            // Wczytanie sceny menu głównego
            GameManager.Instance.LoadScene("Main_Menu");
        }

        // Obsługa kliknięcia przycisku "Quit"
        private void QuitGame()
        {
            // Wyjście z aplikacji
            Application.Quit();
        }
    }
}