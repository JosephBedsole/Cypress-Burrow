using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

    void Update()
    {

        if (Input.GetButtonDown("Fire3"))
        {
            Application.Quit();
        }

    }
}
