using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public float turnSpeed = 3;

    private Animator anim;
    private HealthController health;

    void Start() {
        health = GetComponent<HealthController>();
        health.onHealthChanged += AnimateHealth;
        anim = GetComponent<Animator>();
    }
    void AnimateHealth (float health, float prevHealth, float maxHealth)
    {
        if (health <= 0)
        {
            anim.SetTrigger("dying");
        }
        else if (health < prevHealth)
        {
            anim.SetTrigger("Hit");
        }
    }
    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        anim.SetFloat("forwardVelocity", y * speed);
        anim.SetFloat("turnVelocity", x * speed);
        anim.SetFloat("speed", x * x + y * y);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("slashing");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Hit");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("dying");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("grab");
        }
    }
}
