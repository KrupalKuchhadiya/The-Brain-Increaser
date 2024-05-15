using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeScripts : MonoBehaviour
{

    [SerializeField]
    GameObject HomePanel, LoadingPanel, ExitPanel, SettingPanel;
    [SerializeField]
    Image LoadingSlider;
    [SerializeField]
    float Speed;
    bool LoadingBool;
    [SerializeField]
    Button MusicBtn, SoundBtn;
    [SerializeField]
    Sprite musicON, musicOFF, soundON, soundOFF;
    [SerializeField]
    AudioSource musicSource, soundSource;


    public void Start()
    {
        if (AudioManager.instance.music)
        {

            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            AudioManager.instance.music = true;
        }
        else
        {

            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            AudioManager.instance.music = false;
        }




        if (AudioManager.instance.sound)
        {

            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            AudioManager.instance.sound = true;
        }
        else
        {

            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            AudioManager.instance.sound = false;
        }
    }
    //Basic Sound Play On Click Method 
    public void SoundClick()
    {
        soundSource.Play();
    }
    //SoundManager Method
    public void SoundManagement()
    {
        SoundClick();
        if (AudioManager.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            AudioManager.instance.sound = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            AudioManager.instance.sound = true;
        }
    }
    //MusicManager Method
    public void MusicManagement()
    {
        SoundClick();
        if (AudioManager.instance.music)
        {
            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            AudioManager.instance.music = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            AudioManager.instance.music = true;
        }
    }
    private void Update()
    {
        if (LoadingBool)
        {
            if (LoadingSlider.fillAmount < 1)
            {
                LoadingSlider.fillAmount += Speed * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(HomePanel.activeInHierarchy)
            {
                HomePanel.SetActive(false);
                ExitPanel.SetActive(true);
            }

            if (ExitPanel.activeInHierarchy)
            {
                Application.Quit();
            }

            if (SettingPanel.activeInHierarchy)
            {
                SettingPanel.SetActive(false);
                HomePanel.SetActive(true);
            }
        }
    }
    public void LoadingPanelOpen()
    {
        SoundClick();
        HomePanel.SetActive(false);
        LoadingPanel.SetActive(true);
        LoadingBool = true;
    }
    public void ExitPanelOpen()
    {
        SoundClick();
        HomePanel.SetActive(false);
        ExitPanel.SetActive(true);
    }
    public void ExitPanelYesButton()
    {
        SoundClick();
        HomePanel.SetActive(true);
        ExitPanel.SetActive(false);
    }
    public void ExitPanelNoButton()
    {
        SoundClick();
        HomePanel.SetActive(true);
        ExitPanel.SetActive(false);
    }
    public void SettingPanelOpen()
    {
        SoundClick();
        HomePanel.SetActive(false);
        SettingPanel.SetActive(true);
    }
    public void SettingPanelClose()
    {
        SoundClick();
        HomePanel.SetActive(true);
        SettingPanel.SetActive(false);
    }
}