using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance { get; private set; }

    private Data scoreData;

    public Data ScoreData
    {
        get
        {
            return scoreData;
        }
        private set
        {
            scoreData = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ScoreData = new Data();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class Data
    {
        public string name;

        public int hightScore;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(scoreData);
        File.WriteAllText(Application.persistentDataPath + "/savescore.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savescore.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            scoreData = JsonUtility.FromJson<Data>(json);
        }
    }

    public bool haveData()
    {
        if (ScoreData != null)
            return true;
        return false;
    }


}
