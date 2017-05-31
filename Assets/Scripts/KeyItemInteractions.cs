using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemInteractions : MonoBehaviour {

    public bool near = false;

    public string key;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (near == true)
        {
            if (key == "Lever")
            {
                // Make sure the code after  "&&"  is correct;
                if ((Input.GetKeyDown(KeyCode.E)) && (GameManager.instance.hasLever == true))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    // Animation.Play
                    animator.SetTrigger("pressE");
                }
            }
            else if (key == "KeyCard")
            {
                if ((Input.GetKeyDown(KeyCode.E)) && (GameManager.instance.hasKeyCard == true))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    // Animation.Play
                    animator.SetTrigger("pressE");
                }
            }
            else if (key == "WireCutters")
            {
                if ((Input.GetKeyDown(KeyCode.E)) && (GameManager.instance.hasWireCutters == true))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    // Animation.Play
                    animator.SetTrigger("pressE");
                }
            }
            else if (key == "Torch")
            {
                if ((Input.GetKeyDown(KeyCode.E)) && (GameManager.instance.hasTorch == true))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    // Animation.Play; Burn Something
                    animator.SetTrigger("pressE");
                }
            }
            else
            {
                GameManager.instance.nope.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameManager.instance.pressEToUse.gameObject.SetActive(true);
            near = true; 
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameManager.instance.pressEToUse.gameObject.SetActive(false);
            near = false;
        }
    }

}
