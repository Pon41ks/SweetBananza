using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Facebook.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameSpeed { get; private set; }

    [Header("Properties")]
    public float initialGameSpeed = 10f;
    public float gameSpeedEncrease = 1f;
    private float score;
    private bool canOpenBonus = true;
    private int healthPoints = 4;
    private bool isGameStart;


    [Header("Preference")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private TextMeshProUGUI heartCountText;
    [SerializeField] private TextMeshProUGUI grapeCountText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverGrapeCountText;
    [SerializeField] private GameObject bonus;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject upPanel;
    [SerializeField] private GameObject gameOverPanel;



    private Player player;
    private Spawner spawner;



    private void Awake()
    {

        gameSpeed = 5f;
        SaveData.Current = (SaveData)SerializationManager.Load();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        upPanel.SetActive(false);

        EventManager.OnAnimationEnd.AddListener(ShowGameOverPanel);
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
        Debug.Log(SaveData.Current.highScore);
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

    }


    public void NewGame()
    {
        score = 0;
        isGameStart = true;
        Player.healthPoints = healthPoints;
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        EventManager.SendClearFruits();
        gameSpeed = initialGameSpeed;
        Player.isCanHitting = true;
        Player.isSitting = false;
        enabled = true;

        upPanel.SetActive(true);
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);


        playButton.SetActive(false);
        FaceBookSdk.LogAppInstall();
        gameOverPanel.SetActive(false);

    }

    public void GameOver()
    {

        isGameStart = false;
        UpdateHighScore();
        SerializationManager.Save(SaveData.Current);
        gameSpeed = 0f;
        enabled = false;
        upPanel.SetActive(false);
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);


        gameOverGrapeCountText.text = EventManager.collectedFruits.ToString();
        gameOverScoreText.text = Mathf.RoundToInt(score).ToString("D4");
        if (!EventManager.isNewRecord)
        {
            gameOverPanel.SetActive(true);
        }


    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }


    public void TakingHit()
    {
        gameSpeed -= 1f;
    }
    private void Update()
    {

        if (isGameStart)
        {
            gameSpeed += gameSpeedEncrease * Time.deltaTime;
            score += gameSpeed / 2f * Time.deltaTime;
        }

        grapeCountText.text = EventManager.collectedFruits.ToString();
        scoreText.text = Mathf.RoundToInt(score).ToString("D4");
        heartCountText.text = "4/" + Player.healthPoints.ToString();
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
        if (score > SaveData.Current.highScore)
        {
            SaveData.Current.highScore = score;
            EventManager.SendRecordChanged();
        }
        newRecordText.text = Mathf.RoundToInt(SaveData.Current.highScore).ToString("D5");
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
