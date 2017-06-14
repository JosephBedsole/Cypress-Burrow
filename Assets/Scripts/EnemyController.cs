using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxTurnVelocity = 3.5f;
    public float maxForwardVelocity = 5f;
    public float maxSpeedChange = .3f;
    public float maxTurnSpeedChange = 0.3f;
    public float wanderAmount = 1;
    public float preAttackDistance = 4;
       
    public GameObject weapon;

    private Transform target;
    private Animator anim;
    private HealthController health;
    private Rigidbody body;

    Vector3 targetVelocity;
    Vector3 targetFacing;

    private bool dead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine("WanderCoroutine");
        health = GetComponent<HealthController>();
        health.onHealthChanged += AnimateHealth;
    }

    void AnimateHealth(float health, float prevHealth, float maxHealth)
    {
        if (health <= 0 && prevHealth > 0)
        {
            anim.SetTrigger("dying");
            StopAllCoroutines();
            weapon.gameObject.SetActive(false);
            dead = true;

        }
        else if (health < prevHealth && prevHealth > 0)
        {
            anim.SetTrigger("Hit");
        }
    }
    
    IEnumerator WanderCoroutine()
    {
        while (Vector3.Distance(transform.position, target.position) > 5)
        {
            targetVelocity = transform.forward * maxForwardVelocity + Random.insideUnitSphere * wanderAmount;
            targetFacing = targetVelocity.normalized;
            targetVelocity = transform.right * maxForwardVelocity;
            yield return new WaitForSeconds(.5f);
        }

        StartCoroutine("AttackCoroutine");
    }

    IEnumerator AttackCoroutine()
    {
        Debug.Log("AttackCoroutine");
        HealthController playerHealth = target.gameObject.GetComponent<HealthController>();
        while ( (playerHealth.health > 0) && (Vector3.Distance(transform.position, target.position) < 10) ) 
        {
            yield return StartCoroutine("CirclePlayerState", 1);
            yield return StartCoroutine("CirclePlayerState", -1);
            yield return StartCoroutine("ApproachPlayerState");

            targetVelocity = Vector3.zero;
            Debug.Log("I'm punching you fool!");
            weapon.gameObject.SetActive(true);
            AudioManager.PlayVariedEffect("Whoosh", 1f);
            anim.SetTrigger("slashing");
            yield return new WaitForSeconds(1);
            weapon.gameObject.SetActive(false);
            
        }
    }

    IEnumerator CirclePlayerState(float direction)
    {
        Debug.Log("CirclePlayerState " + direction);
        float startTime = Time.time;
        while (Time.time - startTime < 3)
        {
            Vector3 diff = target.position - transform.position;
            targetFacing = diff.normalized;
            targetVelocity = transform.right * maxForwardVelocity * direction + targetFacing * (diff.magnitude - preAttackDistance);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ApproachPlayerState()
    {
        Debug.Log("ApproachPlayerState");
        while (Vector3.Distance(transform.position, target.position) > 1)
        {
            Vector3 diff = target.position - transform.position;
            targetFacing = diff.normalized;
            targetVelocity = transform.forward * maxForwardVelocity;
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        Vector3 heading = body.velocity.normalized;
        float forward = Vector3.Dot(heading, transform.forward);

        float speed = body.velocity.magnitude;
        anim.SetFloat("speed", speed);
        anim.SetFloat("forwardVelocity", forward * speed);
        anim.SetFloat("turnVelocity", body.angularVelocity.y);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 velocityChange = targetVelocity - body.velocity;
        velocityChange.y = 0;
        velocityChange = velocityChange.normalized * Mathf.Clamp(velocityChange.magnitude, 0, maxSpeedChange);
        body.AddForce(velocityChange, ForceMode.VelocityChange);


        float targetTurnSpeed = Vector3.Cross(transform.forward, targetFacing).y * maxTurnVelocity;
        float turnSpeedChange = targetTurnSpeed - body.angularVelocity.y;
        turnSpeedChange = Mathf.Clamp(turnSpeedChange, -maxTurnSpeedChange, maxTurnSpeedChange);
        body.AddTorque(Vector3.up * turnSpeedChange, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "playerWeapon")
        {
            if (dead == true) return;
            Debug.Log("HERE");
            AudioManager.PlayVariedEffect("Slap", 0.1f);
            health.TakeDamage(2);
        }
    }
}
