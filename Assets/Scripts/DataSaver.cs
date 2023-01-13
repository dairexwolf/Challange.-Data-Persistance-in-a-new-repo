using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance;

    public Text bestScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class Data
    {
        public string name;

        public int hightScore;
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
