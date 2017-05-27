using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       if (Physics.Raycast(ray, 10))
        {
            Vector3 lookHere = ray.GetPoint(10);
            transform.LookAt(new Vector3(lookHere.x, transform.position.y, lookHere.z));
        }
        
        
    }

}
