using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public delegate void OnHealthChanged(float health, float previousHealth, float maxHealth);
    public event OnHealthChanged onHealthChanged = delegate { };

    public delegate void OnAnyHealthChanged(HealthController HealthController, float health, float previousHealth, float maxHealth);
    public static event OnAnyHealthChanged onAnyHealthChanged = delegate { };

    public float maxHealth = 10;
    private float health;


	void Start ()
    {
        health = maxHealth;
	}
	
    public void TakeDamage (float damage)
    {
        float previousHealth = health;
        health -= damage;
        onHealthChanged(health, previousHealth, maxHealth);
        onAnyHealthChanged(this, health, previousHealth, maxHealth);
    }
	
	void Update () {
		
	}
}
