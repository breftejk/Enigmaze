using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartSecene : MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;
        public TMP_InputField usernameInput;

        private string username;
        
        public void Start()
        {
            if(GameManager.Instance.CurrentLevel != 0) 
                GameManager.Instance.LoadScene("Main_Menu");
            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(ExitGame);
            usernameInput.onValueChanged.AddListener(delegate { UsernameChanged(usernameInput); });
        }

        private void UsernameChanged(TMP_InputField textbox)
        {
            username = textbox.text[1].ToString();
        }

        private void StartGame()
        {
            Debug.Log("aaaa");
            GameManager.Instance.SetUsername(username);
            GameManager.Instance.LoadScene("Main_Menu");
            
            Debug.Log(usernameInput.text);
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}