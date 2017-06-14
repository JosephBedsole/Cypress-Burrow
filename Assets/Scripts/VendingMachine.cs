using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {

    public bool near = false;
    public bool hasCoin = false;

    public Transform player;

    void Update()
    {
        if (near == true)
        {
            if (hasCoin == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HealthController playerHealth = player.gameObject.GetComponent<HealthController>();

                    GameManager.instance.pressEToUse.gameObject.SetActive(false);
                    GameManager.instance.youGotFullHealth.gameObject.SetActive(true);
                    playerHealth.health = playerHealth.maxHealth;
                }
            }
            else if (hasCoin == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.instance.pressEToUse.gameObject.SetActive(false);
                    GameManager.instance.youCantDoThat.gameObject.SetActive(true);
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            near = true;
            GameManager.instance.pressEToUse.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            near = false;

            GameManager.instance.pressEToUse.gameObject.SetActive(false);
            GameManager.instance.youGotFullHealth.gameObject.SetActive(false);
            GameManager.instance.youCantDoThat.gameObject.SetActive(false);
        }
    }
}
