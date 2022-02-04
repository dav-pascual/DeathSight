using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Button hudButtonText;
    public AudioMixer mixer;
    public Slider musicSlider, soundSlider;
    bool status;
    float mixerValue;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(true);
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

    public void BackOptionsMenu()
    {
        SceneManager.LoadScene("Menu");
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
