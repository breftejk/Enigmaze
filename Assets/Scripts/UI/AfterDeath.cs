using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AfterDeath : MonoBehaviour
    {
        // Przycisk do ponowienia gry
        public Button tryAgainButton;
        
        // Przycisk do powrotu do menu
        public Button menuButton;

        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            // Dodanie słuchacza zdarzeń do przycisku ponowienia gry
            tryAgainButton.onClick.AddListener(TryAgain);
            
            // Dodanie słuchacza zdarzeń do przycisku powrotu do menu
            menuButton.onClick.AddListener(GoToMenu);
        }

        // Metoda wywoływana po kliknięciu przycisku ponowienia gry
        private void TryAgain()
        {
            // Wczytanie obecnej sceny gry
            GameManager.Instance.LoadScene();
        }

        // Metoda wywoływana po kliknięciu przycisku powrotu do menu
        private void GoToMenu()
        {
            // Wczytanie sceny menu głównego
            GameManager.Instance.LoadScene("Main_Menu");
        }
    }
}