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

    public Text heyImSlime;
    public Text myShopIsComingSoon;

    public Text pressEToOpen;
    public Text pressEToClose;

    public Text pressEToUse;
    public Text pressEToTalk;
    public Text youCantDoThat;

    public Text sceneChangeTitle;
    public Text GameOver;

    private Animator doorOpen;

    void Start()
    {
        StartCoroutine("DisplayTitle");
    }

    IEnumerator DisplayTitle()
    {
        yield return new WaitForSeconds(1);
        sceneChangeTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        sceneChangeTitle.gameObject.SetActive(false);
    }
}
