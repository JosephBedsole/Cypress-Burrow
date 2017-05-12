using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public bool near = false;
    public bool open = false;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update ()
    {
		if (near == true)
        {
            if (open == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.instance.pressEToOpen.gameObject.SetActive(false);
                    GameManager.instance.pressEToClose.gameObject.SetActive(true);
                    // Animation.Play
                    animator.SetTrigger("pressE");
                    open = true;
                }
            }

            else if (open == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    GameManager.instance.pressEToClose.gameObject.SetActive(false);
                    GameManager.instance.pressEToOpen.gameObject.SetActive(true);
                    // Animation.Play
                    animator.SetTrigger("PressE");
                    open = false;
                }
              
            }
        } 
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameManager.instance.pressEToOpen.gameObject.SetActive(true);
            near = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameManager.instance.pressEToOpen.gameObject.SetActive(false);
            GameManager.instance.pressEToClose.gameObject.SetActive(false);
            near = false;
        }
    }

}
