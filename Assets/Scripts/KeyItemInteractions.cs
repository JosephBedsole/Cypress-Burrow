using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KeyItemInteractions : MonoBehaviour {

    public bool near = false;

    public Animator animator;
    public string[] keys;

    // Change the foreach loops into for loops
    public bool CanActivate(string[] inventory)
    {
        foreach (string key in keys)
        {
            bool found = false;
            foreach (string item in inventory)
            {
                if (key == item)
                {
                    Debug.Log("You have the item!");
                    found = true;
                    break;
                }
            }
            if (!found) return false;
            Debug.Log("You didn't have the item!");     
        }
        return true;
    }

    void Update()
    {
        if (near == true)
        {
            if (CanActivate(new string[] { "keyCard" }))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    Debug.Log("I worked!");
                    // Animation.Play
                    animator.Play(0);
                }
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
            GameManager.instance.youCantDoThat.gameObject.SetActive(false);
            near = false;
        }
    }

}
