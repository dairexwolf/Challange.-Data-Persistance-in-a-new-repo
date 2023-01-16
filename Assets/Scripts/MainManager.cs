using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;
    public GameObject BestScoreScreen;
    public GameObject NameInputField;

    private bool m_Started = false;
    private int m_Points;
    private int m_BestScore;

    private bool m_GameOver = false;
    private bool m_TypingName = false;

    private void Awake()
    {
        if (DataSaver.Instance.haveData())
        {
            NameInputField.GetComponent<TMP_InputField>().text = DataSaver.Instance.ScoreData.name;
            m_BestScore = DataSaver.Instance.ScoreData.hightScore;
            UpdateBestScore();
        }
        else
        {
            m_BestScore = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }
        }
        else if (m_GameOver)
        {
            if (m_Points > m_BestScore)
            {
                m_TypingName = true;
                if (!BestScoreScreen.activeInHierarchy)
                {
                    BestScoreScreen.SetActive(true); 
                }
            }
            if (!m_TypingName)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Exit();
                }
            }
        }
    }

    void Exit()
    {
        SceneManager.LoadScene(0);
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
    }

    public void FinishTypeName()
    {
        m_TypingName = false;
        m_BestScore = m_Points;
        DataSaver.Instance.ScoreData.name = NameInputField.GetComponent<TMP_InputField>().text;
        DataSaver.Instance.ScoreData.hightScore = m_BestScore;
        DataSaver.Instance.SaveData();
        UpdateBestScore();
        BestScoreScreen.SetActive(false);
    }

    public void UpdateBestScore()
    {
        BestScoreText.text = "Best Score: " + DataSaver.Instance.ScoreData.name + " - " + m_BestScore;
    }
}
