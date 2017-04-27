using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private void OnCollisionEnter(Collision c)
    {
        HealthController h = c.gameObject.GetComponent<HealthController>();
        if (h != null) h.TakeDamage(1);
    }
}
