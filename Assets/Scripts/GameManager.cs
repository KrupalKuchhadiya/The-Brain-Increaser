using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region Variables Declartion

    #region All Panels Variable
    [Header("All Panels Variable")]
    [SerializeField]
    GameObject SelectionPanel, PlayPanel, SettingPanel, WrongAnswerPanel, TimeOverPanel;
    #endregion

    #region Music ANd Sound Setting Variable
    [Header("Music ANd Sound Setting Variable")]
    [SerializeField]
    Button MusicBtn, SoundBtn;
    [SerializeField]
    Sprite musicON, musicOFF, soundON, soundOFF;
    [SerializeField]
    AudioSource musicSource, soundSource, RightSource, WrongSource;
    #endregion

    #region Field Choise Number
    float SelectedButton;
    #endregion

    #region Play Panel Question Text Variable
    [Header("Play Panel Question Text Variable ")]
    [SerializeField]
    TextMeshProUGUI LeftText, OpratorText, RightText;
    float Value1, Value2, Ans;
    #endregion

    #region Generated Value Variable
    [Header("Generated Value Variable")]
    [SerializeField]
    List<float> GaneratedOptionValue;
    bool flag;
    #endregion

    #region All Four Option Text Variable
    [Header("All Four Option Text Variable")]
    [SerializeField]
    TextMeshProUGUI[] AllFourOptionButton;
    #endregion

    #region TimeLine Variable
    [Header("Timer Variable")]
    [SerializeField]
    Image LoadingSlider;
    float speed = 0.2f;
    bool TimeLineController;
    #endregion

    #endregion


    #region Method

    #region Start For Music Method
    void Start()
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
    #endregion

    #region Update Method For Time Line
    private void Update()
    {
        if (TimeLineController)
        {
            if (LoadingSlider.fillAmount > 0)
            {
                LoadingSlider.fillAmount -= speed * Time.deltaTime;
            }
            else
            {
                TimeOverPanel.SetActive(true);
                PlayPanel.SetActive(false);
            }
        }

        //Escape Button
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(SelectionPanel.activeInHierarchy)
            {
                SceneManager.LoadScene("HomeScene");
            }

            if (PlayPanel.activeInHierarchy)
            {
                PlayPanel.SetActive(false);
                SelectionPanel.SetActive(true);
            }

            if (WrongAnswerPanel.activeInHierarchy)
            {
                WrongAnswerPanel.SetActive(false);
                SelectionPanel.SetActive(true);
            }

            if (TimeOverPanel.activeInHierarchy)
            {
                TimeOverPanel.SetActive(false);
                SelectionPanel.SetActive(true);
            }

            if (SettingPanel.activeInHierarchy)
            {
                SettingPanel.SetActive(false);
                SelectionPanel.SetActive(true);
            }
        }
    }
    #endregion

    #region Right Answer Sound Play On Click Method
    public void RightSoundPlay()
    {
        RightSource.Play();
    }
    #endregion

    #region Wrong Answer Sound Play On Click Method
    public void WrongSoundPlay()
    {
        WrongSource.Play();
    }
    #endregion

    #region Basic Sound Play On Click Method 
    public void SoundClick()
    {
        soundSource.Play();
    }
    #endregion

    #region SoundManager Method
    public void SoundManagement()
    {
        SoundClick();
        if (AudioManager.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            RightSource.mute = true;
            WrongSource.mute = true;
            AudioManager.instance.sound = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            RightSource.mute = false;
            WrongSource.mute = false;
            AudioManager.instance.sound = true;
        }
    }
    #endregion

    #region MusicManager Method
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
    #endregion

    #region Selection Field Method
    public void SelectionMethod(int a)
    {
        SoundClick();
        SelectionPanel.SetActive(false);
        PlayPanel.SetActive(true);
        SelectedButton = a;
        QuestionGeneratorMethod();
    }
    #endregion

    #region Question Generator Method
    public void QuestionGeneratorMethod()
    {
        TimeLineController = true;
        switch (SelectedButton)
        {
            case 1:
                OpratorText.text = "+";
                Value1 = Random.Range(0, 20);
                Value2 = Random.Range(0, 20);
                LeftText.text = Value1.ToString();
                RightText.text = Value2.ToString();
                Ans = Value1 + Value2;
                Debug.Log(Ans);
                flag = false;
                break;
            case 2:
                OpratorText.text = "-";
                Value1 = Random.Range(0, 20);
                Value2 = Random.Range(0, 20);
                if(Value2>Value1)
                {
                    float temp = Value1;
                    Value1 = Value2;
                    Value2 = temp;
                }
                LeftText.text = Value1.ToString();
                RightText.text = Value2.ToString();
                Ans = Value1 - Value2;
                Debug.Log(Ans);
                flag = false;
                break;
            case 3:
                OpratorText.text = "x";
                Value1 = Random.Range(0, 20);
                Value2 = Random.Range(0, 20);
                LeftText.text = Value1.ToString();
                RightText.text = Value2.ToString();
                Ans = Value1 * Value2;
                Debug.Log(Ans);
                flag = false;
                break;
            case 4:
                OpratorText.text = "/";
                Value2 = Random.Range(0, 15);
                Value1 = Value2*Random.Range(0, 15);
                LeftText.text = Value1.ToString();
                RightText.text = Value2.ToString();
                Ans = Value1 / Value2;
                Debug.Log(Ans);
                flag = true;
                break;
        }
        OptionGeneratorMethod();
    }
    #endregion

    #region Option Generator Method
    public void OptionGeneratorMethod()
    {
        float RandomGenerated;
        GaneratedOptionValue.Clear();
        for (int i = 0; i < 3; i++)
        {

            do
            {
                if (flag)
                {
                    RandomGenerated = Random.Range((int)Ans - 5, (int)Ans + 5);
                    while (RandomGenerated <= 0)
                    {
                        RandomGenerated = Random.Range((int)Ans - 5, (int)Ans + 5);
                    }
                    float RandomVal = (int)System.Math.Abs(RandomGenerated);
                    RandomGenerated = RandomVal;
                }
                else
                {
                    RandomGenerated = Random.Range((int)Ans - 5, (int)Ans + 5);
                    float go = System.Math.Abs(RandomGenerated);
                    RandomGenerated = go;
                }
            } while (GaneratedOptionValue.Contains(RandomGenerated) || Ans == RandomGenerated);
            GaneratedOptionValue.Add(RandomGenerated);

        }
        GeneratedOptionSet();
    }
    #endregion

    #region Set Option Generator Method
    public void GeneratedOptionSet()
    {
        int OptionVal;
        int Counter = 0;
        OptionVal = Random.Range(0, AllFourOptionButton.Length);
        for (int i = 0; i < AllFourOptionButton.Length; i++)
        {
            if (i == OptionVal)
            {
                AllFourOptionButton[i].text = Ans.ToString();
            }
            else
            {
                AllFourOptionButton[i].text = GaneratedOptionValue[Counter].ToString();
                Counter++;
            }
        }

    }
    #endregion

    #region Ans Check Method
    public void AnsCheck(TextMeshProUGUI Checker)
    {
        if(Checker.text == Ans.ToString())
        {
            RightSoundPlay();
            QuestionGeneratorMethod();
            LoadingSlider.fillAmount = 1;
        }
        else
        {
            WrongSoundPlay();
            WrongAnswerPanel.SetActive(true);
            PlayPanel.SetActive(false);
            TimeLineController = false;
            Interstitial.instance.ShowInterstitialAd();
        }
    }
    #endregion

    #region Setting Panel Open Method
    public void SettingPanelOpen()
    {
        SoundClick();
        SettingPanel.SetActive(true);
        SelectionPanel.SetActive(false);
    }
    #endregion

    #region Setting Panel Close Method
    public void SettingPanelClose()
    {
        SoundClick();
        SettingPanel.SetActive(false);
        SelectionPanel.SetActive(true);
    }
    #endregion

    #region Play Panel Open Method
    public void PlayPanelOpen()
    {
        SoundClick();
        PlayPanel.SetActive(true);
        SelectionPanel.SetActive(false);
    }
    #endregion

    #region Play Panel Close Method
    public void PlayPanelClose()
    {
        SoundClick();
        PlayPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        TimeLineController = false;
    }
    #endregion

    #region Time Line Over Home Button Method
    public void TimeLineOverHomeBtn()
    {
        SoundClick();
        TimeOverPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        TimeLineController = false;
        LoadingSlider.fillAmount = 1;

    }
    #endregion

    #region Time Line Over Retry Button Method
    public void TimeLineOverRetryBtn()
    {
        SoundClick();
        TimeOverPanel.SetActive(false);
        QuestionGeneratorMethod();
        LoadingSlider.fillAmount = 1;
        PlayPanel.SetActive(true);

    }
    #endregion

    #region Game Over Home Button Method
    public void WAHomeBtn()
    {
        SoundClick();
        WrongAnswerPanel.SetActive(false);
        SelectionPanel.SetActive(true);
       
    }
    #endregion

    #region Game Over Retry Button Method
    public void WARetryBtn()
    {
        SoundClick();
        WrongAnswerPanel.SetActive(false);
        QuestionGeneratorMethod();
        LoadingSlider.fillAmount = 1;
        PlayPanel.SetActive(true);

    }
    #endregion

    #region Back To Home Scene Method
    public void HomeSceneOpen()
    {
        SceneManager.LoadScene("HomeScene");
    }
    #endregion

    #endregion

}