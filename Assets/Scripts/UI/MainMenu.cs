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
        public TMP_Text scoreText;
        
        private void Start()
        {
            if (string.IsNullOrEmpty(GameManager.Instance.Username))
            {
                GameManager.Instance.LoadScene("Start_Scene");
                return;
            }

            UpdateUI();

            resumeButton.onClick.AddListener(ResumeGame);
            resetButton.onClick.AddListener(ResetGame);
            exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
        }

        private void UpdateUI()
        {
            if(usernameText == null || levelText == null)
                return;
            
            usernameText.text = GameManager.Instance.Username;
            levelText.text = $"Level: {GameManager.Instance.CurrentLevel}";
            scoreText.text = $"Score: {ScoreManager.Instance.GetScore()}";
        }

        private void ResumeGame()
        {
            GameManager.Instance.LoadScene();
        }

        private void ResetGame()
        {
            GameManager.Instance.ResetProgress();
            ScoreManager.Instance.ResetScore();
            GameManager.Instance.LoadScene("Start_Scene");
        }
    }
}