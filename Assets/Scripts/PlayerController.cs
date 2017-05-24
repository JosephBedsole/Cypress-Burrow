using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxTurnVelocity = 1f;
    public float maxSpeedChange = .1f;
    public float maxForwardVelocity = 0.2f;
    private Animator anim;
    private HealthController health;
    private Rigidbody body;
    public GameObject weapon;

    private Vector3 targetVelocity;

    void Start() {
        health = GetComponent<HealthController>();
        health.onHealthChanged += AnimateHealth;

        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
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
        Transform cam = Camera.main.transform;
        Vector3 targetRight= Input.GetAxis("Horizontal") * cam.right;
        Vector3 targetForward = Input.GetAxis("Vertical") * cam.forward;

        targetVelocity = targetRight + targetForward;
        targetVelocity.y = 0;
        targetVelocity = targetVelocity.normalized * maxForwardVelocity;

        Vector3 heading = body.velocity.normalized;
        float forward = Vector3.Dot(heading, transform.forward);

        float speed = body.velocity.magnitude;
        anim.SetFloat("speed", speed);
        anim.SetFloat("forwardVelocity", forward * speed);
        anim.SetFloat("turnVelocity", body.angularVelocity.y);


        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        //anim.SetFloat("forwardVelocity", y * speed);
        //anim.SetFloat("turnVelocity", x * speed);
        //anim.SetFloat("speed", x * x + y * y);

        // Actions --------

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine("meleeAttack");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("hurricaneKick");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("dance");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("grab");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("pressE");
        }
    }

    IEnumerator meleeAttack ()
    {
        anim.SetTrigger("slashing");
        maxForwardVelocity = 0;
        weapon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        maxForwardVelocity = 0.2f;
        weapon.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {

        Vector3 v = body.velocity;
        Vector3 heading = v.normalized;
        float speed = v.magnitude;

        Vector3 velocityChange = targetVelocity - v;
        velocityChange = velocityChange.normalized * Mathf.Clamp(velocityChange.magnitude, -maxSpeedChange, maxSpeedChange);
        velocityChange.y = 0;
        body.AddForce(targetVelocity, ForceMode.VelocityChange);

        float turnSpeed = Vector3.Cross(transform.forward, heading).y * maxTurnVelocity;
        body.angularVelocity = Vector3.up * turnSpeed;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == ("enemyWeapon"))
        {
            health.TakeDamage(1);
        }
    }

}
