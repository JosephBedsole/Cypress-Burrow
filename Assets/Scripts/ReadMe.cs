using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMe : MonoBehaviour {
    public static ReadMe read;

    public bool near = false;
    public bool hello = false;



    void Awake()
    {
        if (read == null)
        {
            read = this;
        }
    }

    void Update()
    {
        if (near == true)
        {
            if (hello == false)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    GameManager.instance.pressAButton.gameObject.SetActive(false);
                    GameManager.instance.hello.gameObject.SetActive(true);
                    hello = true;
                }
            }
            else if (hello == true)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    GameManager.instance.hello.gameObject.SetActive(false);
                    hello = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            near = true;
            GameManager.instance.pressAButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            near = false;
            hello = false;
            GameManager.instance.pressAButton.gameObject.SetActive(false);
            GameManager.instance.hello.gameObject.SetActive(false);
        }
    }
}
