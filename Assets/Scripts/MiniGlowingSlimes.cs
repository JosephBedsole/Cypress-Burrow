﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGlowingSlimes : MonoBehaviour {

    public float maxTurnVelocity = 3.5f;
    public float maxForwardVelocity = 5f;
    public float maxSpeedChange = .3f;
    public float maxTurnSpeedChange = 0.3f;
    public float wanderAmount = 1;
    public float preAttackDistance = 4;

    public float knockBack = 20f;

    public float lookAhead = 0;

    public Transform player;
    private HealthController health;   
    private Rigidbody body;
    public ParticleSystem splatter;

    Vector3 targetVelocity;
    Vector3 targetFacing;

    private bool recovering;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        health = GetComponent<HealthController>();
        health.onHealthChanged += AnimateHealth;

        StartCoroutine("Chase");

    }

    void AnimateHealth(float health, float prevHealth, float maxHealth)
    {
        if (health <= 0 && prevHealth > 0)
        {
            gameObject.SetActive(false);
            AudioManager.PlayVariedEffect("Gore", 0.1f);
            ParticleSystem splat = Instantiate(splatter);
            splat.Stop();
            splat.transform.position = transform.position;
            splat.Play();
            // Instantiate blob puddle
        }
        else if (health < prevHealth && prevHealth > 0)
        {
           
        }
    }

    IEnumerator Chase()
    {
        while (Vector3.Distance(transform.position, player.position) < 5)
        {
            Vector3 diff = player.position - transform.position;
            targetFacing = diff.normalized;
            targetVelocity = transform.forward * maxForwardVelocity;


            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (player == null || recovering) return;

        Vector3 target = player.transform.position;
        Vector3 heading = (target - (Vector3)transform.position).normalized;
        body.velocity = heading * maxForwardVelocity;

        
    }

    void FixedUpdate()
    {
        if (player  == null || recovering) return;

        Vector3 velocityChange = targetVelocity - body.velocity;
        velocityChange.y = 0;
        velocityChange = velocityChange.normalized * Mathf.Clamp(velocityChange.magnitude, 0, maxSpeedChange);
        body.AddForce(velocityChange, ForceMode.VelocityChange);


        float targetTurnSpeed = Vector3.Cross(transform.forward, targetFacing).y * maxTurnVelocity;
        float turnSpeedChange = targetTurnSpeed - body.angularVelocity.y;
        turnSpeedChange = Mathf.Clamp(turnSpeedChange, -maxTurnSpeedChange, maxTurnSpeedChange);
        body.AddTorque(Vector3.up * turnSpeedChange, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "playerWeapon")
        {
            // (Enemy position - player position).normalized * knockBack
            body.AddForce( (transform.position - player.transform.position).normalized * knockBack, ForceMode.Impulse);
            StartCoroutine("KnockBackRecovery");

            AudioManager.PlayVariedEffect("Slap", 0.1f);

            health.TakeDamage(2);
        }
    }

    IEnumerator KnockBackRecovery ()
    {
        recovering = true;
        Debug.Log("I'm Recovering!");
        yield return new WaitForSeconds(1);
        recovering = false;
    }
}
