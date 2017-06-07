using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {
    public string sceneToLoad;
    public string buttonToPress = "Fire1";

    public float wait = 1;
    float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetButtonDown(buttonToPress) && (Time.time - startTime) > wait)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
