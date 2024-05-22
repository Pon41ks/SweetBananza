using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float GameSpeed { get; private set; }

    [Header("Properties")]
    public float initialGameSpeed = 10f;
    public float gameSpeedIncrease = 0.1f;
    private float score;
    private bool canOpenBonus = true;
    private const int maxHealthPoints = 4;
    private int healthPoints = maxHealthPoints;
    private bool isGameStart;
   

    [Header("Preference")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private TextMeshProUGUI heartCountText;
    [SerializeField] private TextMeshProUGUI grapeCountText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverGrapeCountText;
    [SerializeField] private GameObject bonus;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject upPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject hardwareButtonHandler;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject settingsPanel;
    
   

    private Player player;
    private Spawner spawner;

    private void Awake()
    {
        GameSpeed = initialGameSpeed;
       
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

        if (player != null)
            player.gameObject.SetActive(false);

        if (spawner != null)
            spawner.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        EventManager.SendGameIsOver(false);
        EventManager.SendCanControl();
        score = 0;
        menuButtons.SetActive(false);
        isGameStart = true;
        Player.healthPoints = healthPoints;
        foreach (var obstacle in FindObjectsOfType<Obstacles>())
        {
            Destroy(obstacle.gameObject);
        }
        EventManager.SendClearFruits();
        GameSpeed = initialGameSpeed;
        Player.isCanHitting = true;
        Player.isSitting = false;
        enabled = true;
        logo.SetActive(false);
        upPanel.SetActive(true);

        if (player != null)
            player.gameObject.SetActive(true);

        if (spawner != null)
            spawner.gameObject.SetActive(true);

        hardwareButtonHandler.SetActive(true);
        playButton.SetActive(false);
        FaceBookSdk.LogAppInstall();
        gameOverPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        foreach (var obstacle in FindObjectsOfType<Obstacles>())
        {
            Destroy(obstacle.gameObject);
        }
        if (EventManager.isPause)
        {
            EventManager.SetGamePaused();
        }
        EventManager.SendGameIsOver(false);
        EventManager.SetPlayerFrozen(false);
        if (player != null)
            player.gameObject.SetActive(false);

        if (spawner != null)
            spawner.gameObject.SetActive(false);

        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
            
        }

        Time.timeScale = 1;
        playButton.SetActive(true);
        GameSpeed = initialGameSpeed;
        menuButtons.SetActive(true);
        logo.SetActive(true);
        hardwareButtonHandler.SetActive(true);
        gameOverPanel.SetActive(false);
        upPanel.SetActive(false);
        

    }

    public void GameOver()
    {
        isGameStart = false;
        UpdateHighScore();
        SerializationManager.Save(SaveData.Current);
        GameSpeed = 0f;
        enabled = false;
        upPanel.SetActive(false);

        if (player != null)
            player.gameObject.SetActive(false);

        if (spawner != null)
            spawner.gameObject.SetActive(false);

        //hardwareButtonHandler.SetActive(false);

        gameOverGrapeCountText.text = EventManager.collectedFruits.ToString();
        gameOverScoreText.text = Mathf.RoundToInt(score).ToString("D4");
        if (!EventManager.isNewRecord)
        {
            
            gameOverPanel.SetActive(true);
            EventManager.SendGameIsOver(true);
        }
    }
   

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void TakingHit()
    {
        GameSpeed -= 1f;
    }

    private void Update()
    {
        if (isGameStart && !EventManager.isPause)
        {
            GameSpeed += gameSpeedIncrease * Time.deltaTime;
            score += GameSpeed / 2f * Time.deltaTime;
            
        }

        grapeCountText.text = EventManager.collectedFruits.ToString();
        scoreText.text = Mathf.RoundToInt(score).ToString("D4");
        heartCountText.text = Player.healthPoints.ToString() + "/" + maxHealthPoints;

        if (score >= 100 && canOpenBonus)
        {
            StartCoroutine(StartBonusGame());
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
        if (!EventManager.isPause)
        {
            canOpenBonus = false;
            bonus.SetActive(true);
            yield return new WaitForSeconds(60);
            canOpenBonus = true;
        }
    }
}
