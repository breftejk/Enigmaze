using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public Button resumeButton;
        public Button resetButton;
        public Button exitButton;

        public TMP_Text usernameText;
        public TMP_Text levelText;
        
        public void Start()
        {
            if (GameManager.Instance.Username == null || GameManager.Instance.CurrentLevel == 0)
                GameManager.Instance.LoadScene("Start_Scene");
            
            resumeButton.onClick.AddListener(ResumeGame);
            resetButton.onClick.AddListener(ResetGame);
            exitButton.onClick.AddListener(ExitGame);

            usernameText.text = GameManager.Instance.Username;
            levelText.text = $"Level: {GameManager.Instance.CurrentLevel}";
        }

        private void ResumeGame()
        {
            GameManager.Instance.LoadScene();
        }

        private void ResetGame()
        {
            GameManager.Instance.ResetProgress();
            GameManager.Instance.LoadScene("Start_Scene");
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}