using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //All Panels Variable
    [Header("All Panels Variable")]
    [SerializeField]
    GameObject SelectionPanel, PlayPanel, SettingPanel, WrongAnswerPanel, TimeOverPanel;
    //Music ANd Sound Setting Variable
    [Header("Music ANd Sound Setting Variable")]
    [SerializeField]
    Button MusicBtn, SoundBtn;
    [SerializeField]
    Sprite musicON, musicOFF, soundON, soundOFF;
    [SerializeField]
    AudioSource musicSource, soundSource, RightSource, WrongSource;

    float SelectedButton;
    //Play Panel Question Text Variable
    [Header("Play Panel Question Text Variable ")]
    [SerializeField]
    TextMeshProUGUI LeftText, OpratorText, RightText;
    float Value1, Value2, Ans;
    //Generated Value Variable
    [Header("Generated Value Variable")]
    [SerializeField]
    List<float> GaneratedOptionValue;
    bool flag;
    //All Four Option Text Variable
    [Header("All Four Option Text Variable")]
    [SerializeField]
    TextMeshProUGUI[] AllFourOptionButton;
    //TimeLine
    [Header("Timer Variable")]
    [SerializeField]
    Image LoadingSlider;
    float speed = 0.2f;
    bool TimeLineController;

  

    // Start For Music Method
    void Start()
    {      

        if (CommonScript.instance.music)
        {

            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
           
            CommonScript.instance.music = true;
        }
        else
        {

            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
           
            CommonScript.instance.music = false;
        }


       

        if (CommonScript.instance.sound)
        {

            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            RightSource.mute = false;
            WrongSource.mute = false;
            CommonScript.instance.sound = true;
        }
        else
        {

            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            RightSource.mute = true;
            WrongSource.mute = true;
            CommonScript.instance.sound = false;
        }
    }
    //Update Method For Time Line
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
    }
    //Basic Sound Play On Click Method 
    public void SoundonClick()
    {
        soundSource.Play();
    }
    //Right Answer Sound Play On Click Method
    public void RightSoundPlay()
    {
        RightSource.Play();
    }
    //Wrong Answer Sound Play On Click Method
    public void WrongSoundPlay()
    {
        WrongSource.Play();
    }
    //SoundManager Method
    public void SoundManagement()
    {
        SoundonClick();
        if (CommonScript.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            RightSource.mute = true;
            WrongSource.mute = true;
            CommonScript.instance.sound = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            RightSource.mute = false;
            WrongSource.mute = false;
            CommonScript.instance.sound = true;
        }
    }
    //MusicManager Method
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
    //Selection Field Method
    public void SelectionMethod(int a)
    {
        SoundonClick();
        SelectionPanel.SetActive(false);
        PlayPanel.SetActive(true);
        SelectedButton = a;
        QuestionGeneratorMethod();
    }
    //Question Generator Method
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
    //Option Generator Method
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
    //Set Option Generator Method
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
    //Ans Check Method
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
        }
    }
    //Setting Panel Open Method
    public void SettingPanelOpen()
    {
        SoundonClick();
        SettingPanel.SetActive(true);
        SelectionPanel.SetActive(false);
    }
    //Setting Panel Close Method
    public void SettingPanelClose()
    {
        SoundonClick();
        SettingPanel.SetActive(false);
        SelectionPanel.SetActive(true);
    }
    //Play Panel Open Method
    public void PlayPanelOpen()
    {
        SoundonClick();
        PlayPanel.SetActive(true);
        SelectionPanel.SetActive(false);
    }
    //Play Panel Close Method
    public void PlayPanelClose()
    {
        SoundonClick();
        PlayPanel.SetActive(false);
        SelectionPanel.SetActive(true);
        TimeLineController = false;
    }
    //Time Line Over Home Button Method
    public void TimeLineOverHomeBtn()
    {
        SoundonClick();
        TimeOverPanel.SetActive(false);
        SelectionPanel.SetActive(true);
       
    }
    //Time Line Over Retry Button Method
    public void TimeLineOverRetryBtn()
    {
        SoundonClick();
        TimeOverPanel.SetActive(false);
        QuestionGeneratorMethod();
        LoadingSlider.fillAmount = 1;
        PlayPanel.SetActive(true);

    }
    //Game Over Home Button Method
    public void WAHomeBtn()
    {
        SoundonClick();
        WrongAnswerPanel.SetActive(false);
        SelectionPanel.SetActive(true);
       
    }
    //Game Over Retry Button Method
    public void WARetryBtn()
    {
        SoundonClick();
        WrongAnswerPanel.SetActive(false);
        QuestionGeneratorMethod();
        LoadingSlider.fillAmount = 1;
        PlayPanel.SetActive(true);

    }
    // Back To Home Scene Method
    public void BSP()
    {
        SceneManager.LoadScene(0);
    }
}
