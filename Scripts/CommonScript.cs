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