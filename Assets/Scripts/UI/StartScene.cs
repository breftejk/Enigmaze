using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScene : MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;
        public TMP_InputField usernameInput;

        private string username;
        
        private void Start()
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.Username))
            {
                GameManager.Instance.LoadScene("Main_Menu");
            }

            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
            usernameInput.onValueChanged.AddListener(UsernameChanged);
        }

        private void UsernameChanged(string text)
        {
            username = text;
        }

        private void StartGame()
        {
            if (string.IsNullOrEmpty(username))
                return;

            GameManager.Instance.SetUsername(username);
            GameManager.Instance.LoadScene("Main_Menu");
        }
    }
}