using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Nazwa użytkownika
    public string Username { get; private set; }
    
    // Aktualny poziom
    public int CurrentLevel { get; private set; } = 1;
    
    // Metoda do zapisywania postępu
    public void SaveProgress()
    {
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
    }
    
    // Metoda do wyjścia z gry
    public void ExitGame()
    {
        Application.Quit();
    }

    // Metoda do ustawienia nazwy użytkownika
    public void SetUsername(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Username = name;
            SaveProgress(); // Zapisz postęp po ustawieniu nazwy użytkownika
        }
    }

    // Metoda do zresetowania postępu
    public void ResetProgress()
    {
        Username = "";
        CurrentLevel = 1; // Reset level to 1
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // Metoda do ładowania sceny
    public void LoadScene(string sceneName = null)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName ?? $"Level_{CurrentLevel}", LoadSceneMode.Single);
    }
    
    // Metoda do ustawienia aktualnego poziomu
    public void SetLevel(int level)
    {
        CurrentLevel = level;
        SaveProgress(); // Zapisz postęp po ustawieniu poziomu
    }
    
    // Metoda wywoływana podczas inicjalizacji skryptu
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Zapobiegaj zniszczeniu tego obiektu podczas ładowania scen
        LoadProgress(); // Załaduj postęp na starcie
    }

    // Metoda do ładowania postępu
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Username"))
            Username = PlayerPrefs.GetString("Username");
        if (PlayerPrefs.HasKey("CurrentLevel"))
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    // Metoda wywoływana podczas zamykania aplikacji
    private void OnApplicationQuit()
    {
        SaveProgress();
    }
}