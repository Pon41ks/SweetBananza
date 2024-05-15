using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        music.volume = musicSlider.value;
    }
    private void Update()
    {
        music.volume = musicSlider.value;
    }
    public void OpenSettings()
    {
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
        EventManager.SendSettingsOpened();
    }
    public void CloseSettings()
    {
        Time.timeScale = 1f;
        EventManager.SendSettingsClosed();
        settingsPanel.SetActive(false);
    }

    public void AddMusicVolume()
    {
        musicSlider.value += 0.2f;
    }
    public void RemoveMusicVolume()
    {
        musicSlider.value -= 0.2f;
    }
    public void AddSoundVolume()
    {
        soundSlider.value += 0.2f;
    }
    public void RemoveSoundVolume()
    {
        soundSlider.value -= 0.2f;
    }


}
