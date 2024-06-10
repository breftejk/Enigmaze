using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public string Username { get; private set; }
    public int CurrentLevel { get; private set; } = 1;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Zapobiegaj zniszczeniu tego obiektu podczas ładowania scen
        LoadProgress(); // Załaduj postęp na starcie
    }

    // Metoda do ustawienia nazwy użytkownika
    public void SetUsername(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Username = name;
            SaveProgress();
        }
    }

    // Metoda do zwiększenia poziomu
    public void IncrementLevel()
    {
        CurrentLevel++;
        SaveProgress();
        LoadScene();
    }

    // Metoda do zresetowania postępu
    public void ResetProgress()
    {
        Username = "";
        CurrentLevel = 1; // Reset level to 0
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void LoadScene(string sceneName = null)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName ?? $"Level_{CurrentLevel}", LoadSceneMode.Single);
    }

    // Metoda do ładowania postępu
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Username"))
            Username = PlayerPrefs.GetString("Username");
        if (PlayerPrefs.HasKey("CurrentLevel"))
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }
}