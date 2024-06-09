using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PausedGame : MonoBehaviour
    {
        public GameObject pausedPanel;  // Panel zawierający elementy UI do wyświetlenia po wciśnięciu ESC
        public Button resumeButton;     // Przycisk "Resume"
        public Button menuButton;       // Przycisk "Menu"
        public Button quitButton;       // Przycisk "Quit"

        void Start()
        {
            if (pausedPanel != null)
                pausedPanel.SetActive(false);

            resumeButton.onClick.AddListener(ResumeGame);
            menuButton.onClick.AddListener(GoToMenu);
            quitButton.onClick.AddListener(QuitGame);
        }

        void Update()
        {
            // Nasłuchiwanie wciśnięcia klawisza ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Pokaż panel po wciśnięciu ESC
                if (pausedPanel != null)
                    pausedPanel.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0f;
            }
        }

        // Obsługa kliknięcia przycisku "Resume"
        void ResumeGame()
        {
            // Ukryj panel i wznow grę
            if (pausedPanel != null)
                pausedPanel.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }

        // Obsługa kliknięcia przycisku "Menu"
        void GoToMenu()
        {
            GameManager.Instance.LoadScene("Main_Menu");
        }

        // Obsługa kliknięcia przycisku "Quit"
        void QuitGame()
        {
            Application.Quit();
        }
    }
}