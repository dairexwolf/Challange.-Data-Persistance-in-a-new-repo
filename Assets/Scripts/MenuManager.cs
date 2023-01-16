using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{

    [SerializeField] Text m_ScoreText;

    private void Awake()
    {
        DataSaver.Instance.LoadData();
        if(DataSaver.Instance.ScoreData != null)
        {
            m_ScoreText.text = "Best Score: " + DataSaver.Instance.ScoreData.name + " - " + DataSaver.Instance.ScoreData.hightScore;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        DataSaver.Instance.SaveData();
    }
}
