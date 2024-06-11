using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScene : MonoBehaviour
    {
        // Przycisk do rozpoczęcia gry
        public Button startButton;

        // Przycisk do wyjścia z gry
        public Button exitButton;

        // Pole do wprowadzenia nazwy użytkownika
        public TMP_InputField usernameInput;

        // Prywatna zmienna przechowująca nazwę użytkownika
        private string username;
        
        // Metoda wywoływana na początku działania skryptu
        private void Start()
        {
            // Sprawdzenie, czy nazwa użytkownika jest już ustawiona
            if (!string.IsNullOrEmpty(GameManager.Instance.Username))
            {
                // Jeśli nazwa użytkownika jest ustawiona, załaduj scenę menu głównego
                GameManager.Instance.LoadScene("Main_Menu");
            }

            // Dodanie słuchaczy zdarzeń do przycisków i pola tekstowego
            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
            usernameInput.onValueChanged.AddListener(UsernameChanged);
        }

        // Metoda wywoływana przy zmianie tekstu w polu do wprowadzenia nazwy użytkownika
        private void UsernameChanged(string text)
        {
            // Aktualizacja prywatnej zmiennej username
            username = text;
        }

        // Metoda wywoływana po kliknięciu przycisku rozpoczęcia gry
        private void StartGame()
        {
            // Sprawdzenie, czy nazwa użytkownika jest pusta
            if (string.IsNullOrEmpty(username))
                return;

            // Ustawienie nazwy użytkownika i załadowanie sceny menu głównego
            GameManager.Instance.SetUsername(username);
            GameManager.Instance.LoadScene("Main_Menu");
        }
    }
}