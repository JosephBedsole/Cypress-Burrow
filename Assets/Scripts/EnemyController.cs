using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float maxTurnVelocity = 3.5f;
    public float maxForwardVelocity = 5f;
    public float maxSpeedChange = .3f;
    public float maxTurnSpeedChange = 0.3f;
    public float wanderAmount = 1;
    public float preAttackDistance = 4;
    public float attackDistance = 1;

    private Transform target;
    private Animator anim;
    private HealthController health;
    private Rigidbody body;

    Vector3 targetVelocity;
    Vector3 targetFacing;

    //Vector3 heading = (target.position - transform.position).normalized;
    //Vector3 targetVelocity = heading * maxForwardVelocity;

    void Start ()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine("WanderCoroutine");
	}

    IEnumerator WanderCoroutine ()
    {
        bool nearTarget = false;

        while(Vector3.Distance(transform.position, target.position) > 5)
        {
            targetVelocity = transform.forward * maxForwardVelocity + Random.insideUnitSphere * wanderAmount;
            targetFacing = targetVelocity.normalized;
            targetVelocity = transform.right * maxForwardVelocity;
            yield return new WaitForSeconds(.5f);

        }
        StartCoroutine("AttackCoroutine");

    }

    private bool attacking = false;

    IEnumerator AttackCoroutine ()
    {
        HealthController playerHealth = target.gameObject.GetComponent<HealthController>();
        while(playerHealth.health > 0)
        {
                Vector3 diff = target.position - transform.position;
                targetFacing = diff.normalized;
            if (!attacking)
            {
                targetVelocity = transform.right * maxSpeedChange + targetFacing * (preAttackDistance - diff.magnitude);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                targetVelocity = transform.forward * maxSpeedChange;
                anim.SetTrigger("attack");
            }
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

    void FixedUpdate ()
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
}
