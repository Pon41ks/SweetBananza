using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameSpeed { get; private set; }

    [Header("Properties")]
    public float initialGameSpeed = 10f;
    public float gameSpeedEncrease = 1f;
    private float score;

    [Header("Preference")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    private Player player;
    private Spawner spawner;

    private void Awake()
    {
        SaveData.Current = (SaveData)SerializationManager.Load();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        SerializationManager.Save(SaveData.Current);
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        NewGame();
    }

    public void NewGame()
    {
        score = 0;

        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;

        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHighScore();
    }

    public void GameOver()
    {
        SerializationManager.Save(SaveData.Current);

        gameSpeed = 0f;

        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHighScore();
    }
    private void Update()
    {
        gameSpeed += gameSpeedEncrease * Time.deltaTime;
        score += gameSpeed  /2f  * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5"); 
    }

    private void UpdateHighScore()
    {
        if(score > SaveData.Current.highScore)
        {
            SaveData.Current.highScore = score;
           
        }
        highScoreText.text = Mathf.RoundToInt(SaveData.Current.highScore).ToString("D5");
    }
}
