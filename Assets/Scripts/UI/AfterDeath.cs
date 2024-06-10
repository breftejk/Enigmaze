using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AfterDeath : MonoBehaviour
    {
        public Button tryAgainButton;
        public Button menuButton;

        void Start()
        {
            tryAgainButton.onClick.AddListener(TryAgain);
            menuButton.onClick.AddListener(GoToMenu);
        }

        void TryAgain()
        {
            GameManager.Instance.LoadScene();
        }

        void GoToMenu()
        {
            GameManager.Instance.LoadScene("Main_Menu");
        }
    }
}