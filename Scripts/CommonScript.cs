using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonScript : MonoBehaviour
{

    public bool music, sound;
    public static CommonScript instance;


   public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
//[SerializeField]
//Button MusicBtn, SoundBtn;
//[SerializeField]
//Sprite musicON, musicOFF, soundON, soundOFF;
//[SerializeField]
//AudioSource musicSource, soundSource;


//void Start()
//{

//    if (CommonScript.instance.music)
//    {

//        MusicBtn.GetComponent<Image>().sprite = musicOFF;
//        musicSource.mute = true;
//        CommonScript.instance.music = false;
//    }
//    else
//    {
//        MusicBtn.GetComponent<Image>().sprite = musicON;
//        musicSource.mute = false;
//        CommonScript.instance.music = true;

//    }



//    if (CommonScript.instance.sound)
//    {
//        SoundBtn.GetComponent<Image>().sprite = soundOFF;
//        soundSource.mute = true;
//        CommonScript.instance.sound = false;
//    }
//    else
//    {

//        SoundBtn.GetComponent<Image>().sprite = soundON;
//        soundSource.mute = false;
//        CommonScript.instance.sound = true;

//    }
//}

//public void SoundonClick()
//{
//    soundSource.Play();
//}


//public void SoundManagement()
//{
//    SoundonClick();
//    if (CommonScript.instance.sound)
//    {
//        SoundBtn.GetComponent<Image>().sprite = soundOFF;
//        soundSource.mute = true;
//        CommonScript.instance.sound = false;
//    }
//    else
//    {
//        SoundBtn.GetComponent<Image>().sprite = soundON;
//        soundSource.mute = false;
//        CommonScript.instance.sound = true;
//    }
//}


//public void MusicManagement()
//{
//    SoundonClick();
//    if (CommonScript.instance.music)
//    {
//        MusicBtn.GetComponent<Image>().sprite = musicOFF;
//        musicSource.mute = true;
//        CommonScript.instance.music = false;
//    }
//    else
//    {
//        MusicBtn.GetComponent<Image>().sprite = musicON;
//        musicSource.mute = false;
//        CommonScript.instance.music = true;
//    }
//}