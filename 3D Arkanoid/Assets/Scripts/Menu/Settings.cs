using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class Settings : MonoBehaviour
    {
        public Slider volumeSlider;
        public Toggle volumeToggle;
        public TMP_Dropdown difficultyDropdown;
        public GameObject optionMenuUI;
        public GameObject mainMenuUI;

        private GameManager gameManager;
        private Animator optionAnimator;

        private void Awake()
        {
        }

        void Start()
        {
            optionAnimator = GetComponent<Animator>();

            volumeToggle.isOn = (PlayerPrefs.GetInt("volumeToggle") != 0);
            volumeSlider.value = PlayerPrefs.GetFloat("volumeSlider", 20f);
            difficultyDropdown.value = PlayerPrefs.GetInt("difficulty");
            
        }

        //save the settings parameters
        public void Save()
        {
            PlayerPrefs.SetFloat("volumeSlider", volumeSlider.value);
            PlayerPrefs.SetInt("volumeToggle", (volumeToggle ? 1 : 0));
            PlayerPrefs.SetInt("difficulty", difficultyDropdown.value);
        }

        public void Back()
        {
            WaitForAnimation();
            mainMenuUI.SetActive(true);
        }    

        private IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(0.6f);
            optionMenuUI.SetActive(false);
        }

    }
}
