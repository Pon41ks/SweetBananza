using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISwitcher
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private UISwitcher switcher1;
        [SerializeField] private UISwitcher switcher2;
        [SerializeField] private AudioSource audioSource1;
        [SerializeField] private AudioSource audioSource2;



        private void Awake()
        {
            // music.volume = musicSlider.value;
            switcher1.onValueChanged.AddListener(OnValueChanged1);
            switcher2.onValueChanged.AddListener(OnValueChanged2);

        }
        private void Update()
        {

            // music.volume = musicSlider.value;
        }

        private void OnValueChanged1(bool isOn)
        {
            if (switcher1.isOn)
            {
                audioSource1.volume = 1;
            }
            else
                audioSource1.volume = 0;
        }
        private void OnValueChanged2(bool isOn)
        {
            if (switcher2.isOn)
            {
                audioSource2.UnPause();
            }
            else audioSource2.Pause();
            Debug.Log("asdasdsa");
        }

        public void OpenSettings()
        {
            EventManager.SetGamePaused();
            EventManager.SetPlayerFrozen(true);
            settingsPanel.SetActive(true);
            EventManager.SendCantControl();
        }
        public void CloseSettings()
        {
            EventManager.SetGamePaused();
            EventManager.SetPlayerFrozen(false);
            EventManager.SendCanControl();
            settingsPanel.SetActive(false);
        }


    }
}

