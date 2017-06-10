using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KeyItemInteractions : MonoBehaviour {

    public bool near = false;

    public Animator animator;

    public string animation;

    public List<string> keysRequired;
    public string[] keysToNeed;
    public string[] itemsToAdd;

    void Start()
    {
        // Adding the keys from the array to the List
        List<string> keysRequired = new List<string>();
        keysRequired.AddRange(keysToNeed);
    }


    //Change the foreach loops into for loops
    public bool CanActivate()
    {
        foreach (string key in keysRequired)
        {
            bool found = false;
            foreach (string item in PlayerInventory.instance.inventory)
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
            if (CanActivate())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AudioManager.PlayVariedEffect("PressButton", 0.1f);
                    GameManager.instance.pressEToUse.gameObject.SetActive(true);
                    Debug.Log("I worked!");
                    // Animation.Play
                    animator.Play(animation);

                    PlayerInventory.instance.AddItems(itemsToAdd);
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
