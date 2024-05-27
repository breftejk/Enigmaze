using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public string Username { get; private set; } = "";
    public int CurrentLevel { get; private set; } = 1;

    private void Awake()
    {
        // Implementacja wzorca Singleton, aby upewnić się, że istnieje tylko jedna instancja tej klasy
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Zapobiegaj zniszczeniu tego obiektu podczas ładowania scen
            LoadProgress(); // Załaduj postęp na starcie
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Metoda do ustawienia nazwy użytkownika
    public void SetUsername(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Username = name;
            PlayerPrefs.SetString("Username", Username);
            PlayerPrefs.Save();
        }
    }

    // Metoda do zwiększenia poziomu
    public void IncrementLevel()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
    }

    // Metoda do zresetowania postępu
    public void ResetProgress()
    {
        Username = "";
        CurrentLevel = 1;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // Metoda do ładowania postępu
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Username")) Username = PlayerPrefs.GetString("Username");
        if (PlayerPrefs.HasKey("CurrentLevel")) CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }
}