using UnityEngine;
using System.IO;


public class PuntuacionAlta : MonoBehaviour
{
    public static PuntuacionAlta Instance;

    public int BestScore { get; private set; }
    class SaveData
    {
        public int BestScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    //Actualizar la puntucion mas alta
    public void UpdateBestScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;
            SaveBestScore();
        }
    }

    //cargar la puntuacion mas alta
    private void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScore = JsonUtility.FromJson<SaveData>(json).BestScore;
        }
    }

    private void SaveBestScore()
    {
        SaveData data = new SaveData { BestScore = BestScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }


}

