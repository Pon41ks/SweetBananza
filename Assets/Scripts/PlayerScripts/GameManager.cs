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
    private bool canOpenBonus = true;
    private bool firstOpenBonus = true;
    private int healthPoints = 4;


    [Header("Preference")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject bonus;
    
    

    public Button retryButton;
    private Player player;
    private Spawner spawner;
    private HealthView health;

    private void Awake()
    {
        gameSpeed = initialGameSpeed;
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
        health = FindAnyObjectByType<HealthView>();
        NewGame();
    }

    public void NewGame()
    {
        score = 0;
        Player.healthPoints = healthPoints;
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        Player.isCanHitting = true;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        health.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHighScore();
    }

    public void GameOver()
    {
        UpdateHighScore();
        SerializationManager.Save(SaveData.Current);

        gameSpeed = 0f;

        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        
    }

    public void TakingHit()
    {
        gameSpeed -= 1f;
    }
    private void Update()
    {
        
            gameSpeed += gameSpeedEncrease * Time.deltaTime;
            score += gameSpeed / 2f * Time.deltaTime;
            scoreText.text = Mathf.RoundToInt(score).ToString("D5");
        if (score >= 100)
        {
            if (canOpenBonus)
            {
                StartCoroutine(StartBonusGame());
            }
        }
            

    }

    private void UpdateHighScore()
    {
        if(score > SaveData.Current.highScore)
        {
            SaveData.Current.highScore = score;
           
        }
        highScoreText.text = Mathf.RoundToInt(SaveData.Current.highScore).ToString("D5");
    }

    private IEnumerator StartBonusGame()
    {
        
        
            canOpenBonus = false;
            bonus.SetActive(true);
            Time.timeScale = 0f;

            yield return new WaitForSeconds(60);
            canOpenBonus = true;
        
          

    }

}
