using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Puntuacion : MonoBehaviour
{


    public Text ScoreText;
    public Text ScoreText2;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    class SaveData
    {
        public int score;
        public int highScore;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objeto"))
        {
            Debug.Log("Objeto en la canasta");
            // Suma 1 punto en lugar de 0 cuando el objeto toca la canasta
            AddPoint(1);
            // Destruye el objeto que tocó la canasta
            Destroy(other.gameObject);
        }
    }


    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveScore();
    }

    public void SaveScore()
    {
        SaveData data = LoadData();
        if (m_Points > data.highScore)
        {
            data.highScore = m_Points;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        /*
        SaveData data = new SaveData();
          data.score = m_Points;
          string json = JsonUtility.ToJson(data);
          File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);*/
    }
    public void LoadScore()
    {

        /*
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            ScoreText2.text = $"Last Score : {data.score}";
        }*/

        SaveData data = LoadData();
        ScoreText2.text = $"Best Score : {data.highScore}";
    }

    private SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return new SaveData();
    }
}
