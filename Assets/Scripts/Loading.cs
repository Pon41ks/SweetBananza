using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingSimulation : MonoBehaviour
{
    [SerializeField] private Image mask;
    public float loadTime = 2f; // Время загрузки в секундах

    private void Start()
    {
        // Запускаем корутину, которая будет симулировать загрузку
        StartCoroutine(SimulateLoading());
    }

    private IEnumerator SimulateLoading()
    {
        float timer = 0f;

        // Пока таймер меньше времени загрузки
        while (timer < loadTime)
        {
            // Увеличиваем таймер
            timer += Time.deltaTime;

            // Вычисляем прогресс загрузки как долю времени, прошедшего от начала загрузки
            float progress = timer / loadTime;

            // Обновляем значение слайдера
            mask.fillAmount = progress;

            yield return null; // Ждем следующего кадра
        }

        SceneManager.LoadScene("Game");
        mask.fillAmount = 1f;
    }
}
