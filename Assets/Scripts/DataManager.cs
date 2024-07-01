using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{   
    // Start is called before the first frame update
    public static DataManager instance;
    public TextMeshProUGUI BestScoreText;
    public InputField inputField;

    public string playerName;
    public int bestScore;
    public string bestPlayer;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // 새 코드의 끝
        instance = this;
        LoadScore();
    }

    [Synchronization]
    class SaveDate
    {
        public int bestScore;
        public string playerName;
        public string bestPlayer;
    }

    public void SaveScore()
    {
        SaveDate data = new SaveDate();
        data.playerName = playerName;
        data.bestScore = bestScore;
        data.bestPlayer = bestPlayer;
        

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDate date = JsonUtility.FromJson<SaveDate>(json);

            playerName = date.playerName;
            bestScore = date.bestScore;
            bestPlayer = date.bestPlayer;

            BestScoreText.text = "BestScore : " + bestPlayer + " - " + bestScore;
            inputField.text = playerName;
        }

    }
 
}
