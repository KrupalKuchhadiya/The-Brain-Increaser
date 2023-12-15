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
        if (CommonScript.instance.music)
        {

            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            CommonScript.instance.music = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            CommonScript.instance.music = true;

        }



        if (CommonScript.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            CommonScript.instance.sound = false;
        }
        else
        {

            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            CommonScript.instance.sound = true;

        }
    }

    public void SoundonClick()
    {
        soundSource.Play();
    }


    public void SoundManagement()
    {
        SoundonClick();
        if (CommonScript.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            CommonScript.instance.sound = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            CommonScript.instance.sound = true;
        }
    }

       
    

   
    public void MusicManagement()
    {
        SoundonClick();
        if (CommonScript.instance.music)
        {
            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            CommonScript.instance.music = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            CommonScript.instance.music = true;
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
    }

    public void LoadingPanelOpen()
    {
        SoundonClick();
        HomePanel.SetActive(false);
        LoadingPanel.SetActive(true);
        LoadingBool = true;
    }

    public void ExitPanelOpen()
    {
        SoundonClick();
        HomePanel.SetActive(false);
        ExitPanel.SetActive(true);
    }

    public void ExitPanelYesButton()
    {
        SoundonClick();
        HomePanel.SetActive(true);
        ExitPanel.SetActive(false);
    }

    public void ExitPanelNoButton()
    {
        SoundonClick();
        HomePanel.SetActive(true);
        ExitPanel.SetActive(false);
    }

    public void SettingPanelOpen()
    {
        SoundonClick();
        HomePanel.SetActive(false);
        SettingPanel.SetActive(true);
    }

    public void SettingPanelClose()
    {
        SoundonClick();
        HomePanel.SetActive(true);
        SettingPanel.SetActive(false);
    }

    


}
