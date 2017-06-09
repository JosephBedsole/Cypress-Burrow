using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public string[] inventory;

    KeyItemInteractions keyItem;

    void Start()
    {
        keyItem = GetComponent<KeyItemInteractions>();
    }

    void GetItem()
    {
       // inventory.AddRange(keyItem.itemsToAdd);
    }

}
