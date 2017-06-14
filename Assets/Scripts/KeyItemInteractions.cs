using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class KeyItemInteractions : MonoBehaviour {

    public bool near = false;

    public Animator animator;

    public string animation;
    public Text textToShow;

    public List<string> keysRequired;
    public string[] itemsToAdd;



    //Change the foreach loops into for loops
    public bool CanActivate()
    {
        Debug.Log("Required count: " + keysRequired.Count);
        Debug.Log("inventory count: " + PlayerInventory.instance.inventory.Count);
        foreach (string key in keysRequired)
        {
            bool found = false;
            foreach (string item in PlayerInventory.instance.inventory)
            {
                if (key == item)
                {
                    Debug.Log("You have the item!");
                    found = true;
                }
            }
            if (!found)
            {
                return false;
            }
            
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
                    Debug.Log("I worked!");
                    // Animation.Play
                    animator.Play(animation);
                    StartCoroutine("DisplayText");

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

    IEnumerator DisplayText()
    {
        GameManager.instance.pressEToUse.gameObject.SetActive(false);
        textToShow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textToShow.gameObject.SetActive(false);
    }
}
