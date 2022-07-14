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
    public const int maxHealth = 100;
    public const int playerNoticeTreshold = 120;
    public const float detectDistance = 30f;
    public const int damage = 5;
    public const int damageDelay = 30;

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
            Debug.Log(gameObject.name + " CAN SEE PLAYER - " + playerNoticing);
            playerNoticing = Mathf.Clamp(playerNoticing + 4, 0, playerNoticeTreshold);
        }
        else
        {
            playerNoticing = Mathf.Clamp(playerNoticing - 8, 0, playerNoticeTreshold);
        }

        switch (state)
        {
            case EnemyState.idle:
                if (playerNoticing >= playerNoticeTreshold)
                {
                    state = EnemyState.chasing;
                    agent.autoBraking = true;
                }
                else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }

                break;
            case EnemyState.chasing:
                agent.destination = playerTarget.transform.position;

                if ((playerTarget.transform.position - transform.position).magnitude < 2f && currentDamageDelay == 0)
                {
                    player.Damage(damage);
                    currentDamageDelay = damageDelay;
                }

                if (playerNoticing == 0)
                {
                    state = EnemyState.idle;
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

        health -= damage;

        if (health <= 0)
        {
            health = 0;
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

        agent.destination = waypoints[Random.Range(0, waypoints.Length - 1)].position;
    }
    void Kill()
    {
        state = EnemyState.dead;
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        //animator.Play("Z_FallingBack");
        agent.isStopped = true;
        //alertSprite.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
