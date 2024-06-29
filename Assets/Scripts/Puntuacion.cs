using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Drawing;
using UnityEditor;

public class Puntuacion : MonoBehaviour
{
    public Text ScoreText;
    public Text ScoreText2;
    private int m_Points = 0;

    class SaveData
    {
        public int score;
        public int highScore;
    }

    void Start()
    {
        LoadScore();

    }
    private void Update()
    {
      

    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objeto"))
        {
            // Incrementa el puntaje
            m_Points++;
            ScoreText.text = "Score: " + m_Points;
            Destroy(other.gameObject);
        }
        else
        if (other.gameObject.CompareTag("Dange"))
        {
            GameOver();
        }
       
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        //  MainManager.Instance.SaveColor();
    }



    private void GameOver()
    {
        PuntuacionAlta.Instance.TryUpdateHighScore(m_Points);
        SaveScore();
        SceneManager.LoadScene(0);
    }


    public void SaveScore()
    {
        
        SaveData data = new SaveData();
          data.score = m_Points;
          string json = JsonUtility.ToJson(data);
          File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            ScoreText2.text = $"Last Score : {data.score}";
        }
        // Añade esta línea para mostrar la puntuación más alta
        ScoreText2.text += $"\nBest Score: {PuntuacionAlta.Instance.HighScore}";
    }


}
