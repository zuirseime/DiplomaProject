using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    float volume;
    public GameObject slider;

    public void Start()
    {
        volume = slider.GetComponent<Slider>().value;
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
