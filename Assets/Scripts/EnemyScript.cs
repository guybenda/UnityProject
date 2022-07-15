using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    idle,
    chasing,
    dead
}

public class EnemyScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int playerNoticeTreshold = 120;
    public float detectDistance = 30f;
    public int damage = 5;
    public int damageDelay = 30;
    public float normalSpeed = 2f;
    public float chaseSpeed = 4f;

    public Transform[] waypoints;

    private NavMeshAgent agent;
    private PlayerScript player;
    private GameObject playerTarget;
    private Animator animator;
    private EnemyState state = EnemyState.idle;
    private int playerNoticing = 0;
    private int currentDamageDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 7;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        playerTarget = GameObject.FindWithTag("EnemyTarget");

        animator.SetBool("isAttacking", false);
        animator.SetInteger("health", health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == EnemyState.dead) return;

        currentDamageDelay = Mathf.Clamp(currentDamageDelay - 1, 0, damageDelay);

        if (TryFindPlayer())
        {
            //Debug.Log(gameObject.name + " CAN SEE PLAYER - " + playerNoticing);
            playerNoticing = Mathf.Clamp(playerNoticing + 4, 0, playerNoticeTreshold);
        }
        else
        {
            playerNoticing = Mathf.Clamp(playerNoticing - 8, 0, playerNoticeTreshold);
        }

        switch (state)
        {
            case EnemyState.idle:
                agent.speed = normalSpeed;

                if (playerNoticing >= playerNoticeTreshold)
                {
                    state = EnemyState.chasing;
                    animator.SetBool("isAttacking", true);
                    agent.autoBraking = true;
                }
                else if (!agent.pathPending && agent.remainingDistance < 2f)
                {
                    GotoNextPoint();
                }

                break;
            case EnemyState.chasing:
                agent.destination = playerTarget.transform.position;
                agent.speed = chaseSpeed;

                if ((playerTarget.transform.position - transform.position).magnitude < 2f)
                {
                    animator.SetBool("isDamaging", true);

                    if (currentDamageDelay == 0)
                    {

                        player.Damage(damage);
                        currentDamageDelay = damageDelay;
                    }
                }
                else
                {
                    animator.SetBool("isDamaging", false);
                }

                if (playerNoticing == 0)
                {
                    state = EnemyState.idle;
                    animator.SetBool("isAttacking", false);
                    agent.autoBraking = false;
                    GotoNextPoint();
                }
                break;
        }
    }

    public bool Damage(int damage)
    {
        if (state == EnemyState.dead)
            return false;

        health = Mathf.Clamp(health - damage, 0, maxHealth);

        animator.SetInteger("health", health);

        if (health == 0)
        {
            Kill();
            return true;
        }

        return false;
    }
    bool TryFindPlayer()
    {
        const int mask = ~(1 << 7);
        Debug.DrawRay(transform.position, playerTarget.transform.position - transform.position, Color.red, 1f);
        //if (Physics.Raycast(transform.position + Vector3.up * 2f, (playerCamera.transform.position - Vector3.up * 3) - transform.position, out RaycastHit hit, detectDistance, mask))
        if (Physics.Raycast(transform.position, playerTarget.transform.position - transform.position, out RaycastHit hit, detectDistance, mask))
        {
            //hit.collider.gameObject.transform.localScale = Vector3.zero;
            if (hit.collider.gameObject.layer == 6)
            {
                return true;
            }
        }

        return false;
    }

    void GotoNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[Random.Range(0, waypoints.Length)].position;
    }
    void Kill()
    {
        state = EnemyState.dead;
        StartCoroutine(DieRoutine());
        player.enemiesKilled++;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    IEnumerator DieRoutine()
    {
        agent.isStopped = true;
        for (int i = 0; i < 20; i++)
        {
            agent.baseOffset -= 0.3f;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
