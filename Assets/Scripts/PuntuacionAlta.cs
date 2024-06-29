using UnityEngine;
using System.IO;

public class PuntuacionAlta : MonoBehaviour
{
    public static PuntuacionAlta Instance;

    public int HighScore { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void TryUpdateHighScore(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
            SaveHighScore();
        }
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore = JsonUtility.FromJson<SaveData>(json).highScore;
        }
    }

    private void SaveHighScore()
    {
        SaveData data = new SaveData { highScore = HighScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    class SaveData
    {
        public int highScore;
    }
}

