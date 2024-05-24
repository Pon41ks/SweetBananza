using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Serialization;

public class WheelOfFortune : MonoBehaviour
{
    [Header("Preference")] [SerializeField]
    private Animate animate;

    [SerializeField] private Image wheel;
    [SerializeField] private AnimationCurve spinCurve;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject wheelPanel;
    [SerializeField] private GameObject winOrLosePanel;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject spinButton;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI winOrLoseText;

    [Header("Properties")] [SerializeField]
    private float spinDuration = 4f;

    [SerializeField] private int numberOfSectors;
    [SerializeField] private float[] winningAngles;
    private float startAngle;
    private bool isSpinning = false;

    void Start()
    {
        startAngle = wheel.transform.eulerAngles.z;
    }

    private void OnEnable()
    {
        continuePanel.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);
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
        StartCoroutine(Spin());
    }

    public void OnSpinButton()
    {
        spinButton.SetActive(true);
    }

    private IEnumerator Spin()
    {
        float elapsedTime = 0f;

        // Выбираем случайный сектор
        int selectedSector = Random.Range(0, numberOfSectors);
        float endAngle =
            (selectedSector * (360f / numberOfSectors)) + (360f * 5f); // Вращение плюс несколько полных оборотов

        Debug.Log("Spin started, aiming for sector " + selectedSector);

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, spinCurve.Evaluate(elapsedTime / spinDuration));
            wheel.transform.eulerAngles = new Vector3(0, 0, currentAngle);
            yield return null;
        }

        // Останавливаемся точно на середине выбранного сектора
        startAngle = selectedSector * (360f / numberOfSectors);
        wheel.transform.eulerAngles = new Vector3(0, 0, startAngle);

        Debug.Log("Spin finished on sector " + selectedSector);

        yield return new WaitForSeconds(1);
        DeterminePrize(startAngle);

        isSpinning = false;
        yield return new WaitForSeconds(3);
        continuePanel.SetActive(true);
    }

    public void Continue()
    {
        if (continuePanel.activeInHierarchy)
        {
            wheelPanel.SetActive(false);
            winOrLosePanel.SetActive(false);
            transform.rotation = Quaternion.identity;
            spawner.SetActive(true);
        }
    }

    private void DeterminePrize(float finalAngle)
    {
        winOrLosePanel.SetActive(true);
        Debug.Log("Final Angle: " + finalAngle);

        // Нормализуем угол, чтобы он был в пределах от 0 до 360 градусов
        finalAngle = (finalAngle + 360) % 360;

        // Определяем номер сектора, на котором остановилось колесо
        float sectorSize = 360f / numberOfSectors; // Вычисляем размер каждого сектора
        int sector = Mathf.FloorToInt(finalAngle / sectorSize);

        // Проверяем, попал ли сектор в массив выигрышных секторов
        bool isWinningSector = false;
        foreach (float angle in winningAngles)
        {
            int winningSector = Mathf.FloorToInt(angle / sectorSize);
            if (sector == winningSector)
            {
                isWinningSector = true;
                break;
            }
        }

        // Отображаем результат в соответствии с попаданием в выигрышный сектор
        if (isWinningSector)
        {
            winText.SetActive(true);
            Debug.Log("Вы выиграли!");
        }
        else
        {
            loseText.SetActive(true);
            Debug.Log("Вы проиграли!");
        }
    }



/*
private void DeterminePrize(float finalAngle)
{
    winOrLosePanel.SetActive(true);
    Debug.Log("Final Angle: " + finalAngle);

    // Нормализуем угол, чтобы он был в пределах от 0 до 360 градусов
    finalAngle = finalAngle % 360;
    if (finalAngle < 0)
    {
        finalAngle += 360;
    }

    // Определяем, в каком секторе остановилось колесо
    int numberOfSectors = 6; // У нас 6 секторов
    float sectorSize = 360f / numberOfSectors; // Вычисляем размер каждого сектора

    // Округляем конечный угол до ближайшего кратного 60 градусам
    float roundedAngle = Mathf.Round(finalAngle / 60f) * 60f;

    // Проверяем, попал ли округленный угол в один из углов выигрыша
    bool isWinningAngle = false;
    foreach (float angle in winningAngles)
    {
        if (Mathf.Abs(roundedAngle - angle) < 1f) // Проверяем разницу в 1 градус
        {
            isWinningAngle = true;
            break;
        }
    }

    // Отображаем результат в соответствии с попаданием в выигрышный угол
    if (isWinningAngle)
    {
       /// winOrLoseText.text = "Вы выиграли!";
        winText.SetActive(true);
        if (Player.healthPoints < 4)
        {
            Player.healthPoints++;
        }
        Debug.Log("Вы выиграли!");
    }
    else
    {
        loseText.SetActive(true);
        //winOrLoseText.text = "Вы проиграли!";
        Debug.Log("Вы проиграли!");
    }

    */

}