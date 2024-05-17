using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WheelOfFortune : MonoBehaviour
{
    [Header("Preference")]
    [SerializeField] private Animate animate;
    
    [SerializeField] private Image wheel;
    [SerializeField] private AnimationCurve spinCurve;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject wheelPanel;
    [SerializeField] private GameObject winOrLosePanel;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private GameObject winOrLoseTextObj;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI winOrLoseText;


    [Header("Properties")]
    [SerializeField] private float spinDuration = 4f;
    [SerializeField] private string[] prizes; // Массив призов
    [SerializeField] private int[] winningSectors;
    private float startAngle;
    private bool isSpinning = false; // Флаг состояния вращения
    private int healthpoints;

    void Start()
    {
        startAngle = wheel.transform.eulerAngles.z;
        healthpoints = Player.healthPoints;

    }
    private void OnEnable()
    {
        EventManager.SendGamePaused();
        spawner.SetActive(false);
        animator.SetTrigger("OpenWheel");
    }

    public void StartSpin()
    {
        if (isSpinning)
        {
            Debug.Log("Spin already in progress");
            return;
        }

        isSpinning = true;
        Debug.Log("StartSpin called");
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        transform.rotation = Quaternion.identity;
        float elapsedTime = 0f;
        float endAngle = Random.Range(0f, 360f) + 360f * 5f;
        Debug.Log("Spin started");
       
        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, spinCurve.Evaluate(elapsedTime / spinDuration));
            wheel.transform.eulerAngles = new Vector3(0, 0, currentAngle);
            yield return null;
        }

        startAngle = wheel.transform.eulerAngles.z;
        Debug.Log("Spin finished");

        yield return new WaitForSeconds(1);
        DeterminePrize(startAngle);
        
        isSpinning = false;
        yield return new WaitForSeconds(3);
        continuePanel.SetActive(true);
        winOrLoseTextObj.SetActive(false);
        
    }

    public void Continue()
    {
        if (continuePanel.activeInHierarchy)
        {
            EventManager.SendContinueGame();
            wheelPanel.SetActive(false);
            winOrLosePanel.SetActive(false);
            transform.rotation = Quaternion.identity;
            EventManager.SendPlayerUnFrozen();
            EventManager.SendGameUnPaused();
            spawner.SetActive(true);
        }
    }

    private void DeterminePrize(float finalAngle)
    {
        winOrLosePanel.SetActive(true);
        finalAngle = finalAngle % 360;


        int numberOfSectors = 5;


        float sectorAngle = 360f / numberOfSectors;


        int winningSector = Mathf.FloorToInt(finalAngle / sectorAngle);


        bool isWinningSector = false;

        for (int i = 0; i < winningSectors.Length; i++)
        {
            if (winningSectors[i] == winningSector)
            {
                isWinningSector = true;
                break;
            }
        }

        
        // Обновляем жизни игрока при необходимости
        if (isWinningSector && healthpoints < 4)
        {
            winOrLoseText.text = "Вы выйграли!";
            healthpoints++;
            Debug.Log("Вы выиграли и получили дополнительную жизнь! Жизни: " + healthpoints);
        }
        else if (isWinningSector)
        {
            winOrLoseText.text = "Вы выйграли, но у вас уже максимальное кол-во жизней";
            Debug.Log("Вы выиграли, но у вас уже максимальное количество жизней. Жизни: " + healthpoints);
        }
        else
        {
            winOrLoseText.text = "Вы проиграли(";
            Debug.Log("Попробуйте еще раз! Жизни: " + healthpoints);
        }
        
        
        
        
        

    }

}

