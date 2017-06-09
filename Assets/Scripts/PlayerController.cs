using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Global Variables

    public float maxTurnVelocity = 1f;
    public float maxSpeedChange = .1f;
    public float maxForwardVelocity = 0.2f;
    private Vector3 targetVelocity;

    private Animator anim;
    private HealthController health;
    private Rigidbody body;

    public GameObject weapon;
    public GameObject slime;

    public bool inContact = false;

    

    void Start() {
        health = GetComponent<HealthController>();
        health.onHealthChanged += AnimateHealth;

        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

    }

    void Update() {
        Transform cam = Camera.main.transform;
        Vector3 targetRight = Input.GetAxis("Horizontal") * cam.right;
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
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("grab");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("pressE");
        }
    }

    void FixedUpdate()
    {

        Vector3 v = body.velocity;
        Vector3 heading = v.normalized;
        float speed = v.magnitude;

        Vector3 velocityChange = targetVelocity - v;
        velocityChange = velocityChange.normalized * Mathf.Clamp(velocityChange.magnitude, 0, maxSpeedChange);
        velocityChange.y = 0;
        body.AddForce(velocityChange, ForceMode.VelocityChange);

        float turnSpeed = Vector3.Cross(transform.forward, heading).y * maxTurnVelocity;
        body.angularVelocity = Vector3.up * turnSpeed;
    }

    void AnimateHealth(float health, float prevHealth, float maxHealth)
    {
        if (health <= 0 && prevHealth > 0)
        {
            anim.SetTrigger("dying");
            StopAllCoroutines();

        }
        else if (health < prevHealth && prevHealth > 0)
        {
            anim.SetTrigger("Hit");
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Slime")
        {
            health.TakeDamage(1);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == ("enemyWeapon"))
        {
            health.TakeDamage(1);
        }

        //if (c.gameObject.tag == "Slime" && !inContact)
        //{
        //    Debug.Log("Hello! to you sir");
        //    inContact = true;
        //    StartCoroutine("SlimeDamage");
        //}
    }

    //private void OnTriggerExit(Collider c)
    //{
    //    if (c.gameObject.tag == "Slime" && inContact)
    //    {
    //        inContact = false;
    //        StopCoroutine("SlimeDamage");
    //    }
    //}

    IEnumerator meleeAttack()
    {
        anim.SetTrigger("slashing");
        targetVelocity = Vector3.zero;
        weapon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        weapon.gameObject.SetActive(false);
    }
    // This Coroutine has not been started yet! Start it when you know what you are doing!
    IEnumerator SlimeDamage()
    {
        while (inContact)
        {
            Debug.Log("I'm damaging you!");
            health.TakeDamage(1);
            yield return new WaitForSeconds(5);
        }
    } 
}
