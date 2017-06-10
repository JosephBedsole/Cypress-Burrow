using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public static PlayerInventory instance;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // first part declares the code / after " = " / you're creating the code
    public List<string> inventory = new List<string>();

    void Start()
    {
    }

    public void AddItems(string[] itemsToAdd)
    {
        // array.AddRange requires a List to ADD to!
        inventory.AddRange(itemsToAdd);
    }

}
