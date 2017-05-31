using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public void Awake()
    {
        if (instance != true)
        {
            instance = this;
        } 
    }

    public Text pressAButton;
    public Text hello;

    public Text pressEToOpen;
    public Text pressEToClose;

    public Text pressEToUse;

    private Animator doorOpen;

    // Key Item bools

    public bool hasLever = false;
    public bool hasWrench = false;
    public bool hasWireCutters = false;
    public bool hasTorch = false;
    public bool hasKey = false;
}
