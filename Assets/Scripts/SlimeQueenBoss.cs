using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeQueenBoss : MonoBehaviour {

    public float maxTurnVelocity = 3.5f;
    public float maxForwardVelocity = 5f;
    public float maxSpeedChange = .3f;
    public float maxTurnSpeedChange = 0.3f;
    public float wanderAmount = 1;
    public float preAttackDistance = 4;
    public float slingVelocity = 10f;

    public float knockBack = 20f;

    public float lookAhead = 0;

    public Transform player;
    private HealthController health;
    private Rigidbody body;
    public ParticleSystem splatter;
    public Animator anim;
    public Animation jump;

    Vector3 targetVelocity;
    Vector3 targetFacing;
    public string[] itemsToAdd;

    public Text textToShow;

    private bool recovering;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        health = GetComponent<HealthController>();
        anim = GetComponent<Animator>();
        health.onHealthChanged += AnimateHealth;
        StartCoroutine("AttackCoroutine");
        
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
            StartCoroutine("DisplayText");
            PlayerInventory.instance.AddItems(itemsToAdd);
        }
        else if (health < prevHealth && prevHealth > 0)
        {

        }
    }

    void Update()
    {
        if (player == null || recovering) return;

        Vector3 diff = player.position - transform.position;
        targetFacing = diff.normalized;

        Vector3 target = player.transform.position;
        Vector3 heading = (target - (Vector3)transform.position).normalized;
        body.velocity = heading * maxForwardVelocity;


    }

    void FixedUpdate()
    {
        if (player == null || recovering) return;

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
            body.AddForce((transform.position - player.transform.position).normalized * knockBack, ForceMode.Impulse);
            StartCoroutine("KnockBackRecovery");

            AudioManager.PlayVariedEffect("Slap", 0.1f);

            health.TakeDamage(2);
        }
    }

    IEnumerator KnockBackRecovery()
    {
        recovering = true;
        Debug.Log("I'm Recovering!");
        yield return new WaitForSeconds(1);
        recovering = false;
    }

    IEnumerator AttackCoroutine()
    {
        HealthController playerHealth = player.gameObject.GetComponent<HealthController>();
        while ((playerHealth.health > 0)) //&& (Vector3.Distance(transform.position, player.position) < 10))
        {
            Debug.Log("The player is close to me");
            yield return new WaitForSeconds(10);
            StartCoroutine("SlingAttack");
        }
    }

    IEnumerator SlingAttack()
    {
        int count = 2;
        for (int i = 0; i < count; ++i)
        {
            Debug.Log("SlingAttack");
            recovering = true;
            yield return new WaitForSeconds(1);
            // Play the SlingShotAnim
            body.velocity = transform.forward * slingVelocity;
            yield return new WaitForSeconds(2);
        }
    }

    //IEnumerator JumpAttack()
    //{
    //    Debug.Log("JumpAttack");
    //    recovering = true;
    //    anim.Play("JumpingAnim");
    //    yield return new WaitForSeconds(1);
    //    recovering = false;
    //    yield return new WaitForSeconds(3);
    //    recovering = true;
    //    yield return new WaitForSeconds(1);
    //    recovering = false;
    //}

    IEnumerator DisplayText()
    {
        GameManager.instance.pressEToUse.gameObject.SetActive(false);
        textToShow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textToShow.gameObject.SetActive(false);
    }
}
