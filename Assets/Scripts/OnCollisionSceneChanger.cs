using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionSceneChanger : MonoBehaviour {

    public string sceneToLoad;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
