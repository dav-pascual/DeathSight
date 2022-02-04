using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameMenu : MonoBehaviour
{
    public GameObject mainPanel, optionsPanel;
    public Button hudButtonText;
    public AudioMixer mixer;
    public Slider musicSlider, soundSlider;
    bool status;
    float mixerValue;

    void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        PlayerPrefs.SetString("hudState", hudButtonText.GetComponentInChildren<Text>().text);

        status = mixer.GetFloat("musicVolume", out mixerValue);  // Update slider with old value
        if (status)
        {
            musicSlider.value = mixerValue;
        }
        status = mixer.GetFloat("soundsVolume", out mixerValue);  // Update slider with old value
        if (status)
        {
            soundSlider.value = mixerValue;
        }
    }

    public void GameScene()
    {
        SceneManager.LoadScene("History");
    }

    public void OptionsMenu()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackOptionsMenu()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HudButton()
    {
        if (hudButtonText.GetComponentInChildren<Text>().text == "ON")
        {
            hudButtonText.GetComponentInChildren<Text>().text = "OFF";
        }
        else
        {
            hudButtonText.GetComponentInChildren<Text>().text = "ON";
        }
        PlayerPrefs.SetString("hudState", hudButtonText.GetComponentInChildren<Text>().text);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("musicVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        mixer.SetFloat("soundsVolume", volume);
    }
}
