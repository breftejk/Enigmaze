using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Win : MonoBehaviour
    {
        public Button menuButton;       // Przycisk "Menu"
        public Button quitButton;       // Przycisk "Quit"

        private void Start()
        {
            menuButton.onClick.AddListener(GoToMenu);
            quitButton.onClick.AddListener(QuitGame);
        }
        // Obsługa kliknięcia przycisku "Menu"
        private void GoToMenu()
        {
            GameManager.Instance.LoadScene("Main_Menu");
        }

        // Obsługa kliknięcia przycisku "Quit"
        private void QuitGame()
        {
            Application.Quit();
        }
    }
}