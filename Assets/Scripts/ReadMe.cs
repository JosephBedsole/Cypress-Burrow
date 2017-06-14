using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMe : MonoBehaviour {
    //public static ReadMe read;

    //public bool near = false;
    //public bool hey = false;
    //public bool shop = false;




    //void Awake()
    //{
    //    if (read == null)
    //    {
    //        read = this;
    //    }
    //}

    //void Update()
    //{
    //    if (near == true)
    //    {
    //        if (hey == false)
    //        {
    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                GameManager.instance.pressEToTalk.gameObject.SetActive(false);
    //                GameManager.instance.heyImSlime.gameObject.SetActive(true);
                    
    //                hey = true;
    //            }
    //        }
    //        else if (hey == true && shop == false)
    //        {
    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                GameManager.instance.heyImSlime.gameObject.SetActive(false);
    //                GameManager.instance.myShopIsComingSoon.gameObject.SetActive(true);
    //                hey = false;
    //            }
    //        }
    //        else if (shop == true)
    //        {
    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                GameManager.instance.myShopIsComingSoon.gameObject.SetActive(false);
    //                shop = false;
    //            }
    //        }
    //    }
    //}

    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.gameObject.tag == "Player")
    //    {
    //        near = true;
    //        GameManager.instance.pressEToTalk.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider c)
    //{
    //    if (c.gameObject.tag == "Player")
    //    {
    //        near = false;
    //        hey = false;
    //        shop = false;

    //        GameManager.instance.pressEToTalk.gameObject.SetActive(false);
    //        GameManager.instance.heyImSlime.gameObject.SetActive(false);
    //        GameManager.instance.myShopIsComingSoon.gameObject.SetActive(false);
    //    }
    //}
}
