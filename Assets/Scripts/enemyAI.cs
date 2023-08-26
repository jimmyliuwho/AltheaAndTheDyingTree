using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class enemyAI : MonoBehaviour
{

    private enum State
    {
        //Idle,
        Patrolling,
        ChaseTarget,
    }

    //public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject[] waypoints;
    public int currWaypoint = 0;
    private Animator anim;

    public float speed;

    private State state;

    private GameObject target;

    //private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;
        //target = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Player");
        setNextWaypoint();
    }

    private Vector3 GetPatrollingPosition()
    {
        return transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //target = GameObject.FindWithTag("Player");

        //Debug.Log(target.transform.position);
        /*if (agent.remainingDistance == 0 && (agent.pathPending == false))
        {*/
            setNextWaypoint();
        /*}*/
/*        if (agent.remainingDistance <= 0.5)
        {
            anim.SetBool("isIdle", true);
        }

        if (agent.remainingDistance > 0.5)
        {
            anim.SetBool("isIdle", false);
        }*/
        //Debug.Log(speed);

/*        if (anim.GetBool("isDead") == true)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }*/
    }

    private void setNextWaypoint()
    {
        switch (state)
        {
            default:
            case State.Patrolling:
                FindTarget();
                //Debug.Log("patrolling");
                if (currWaypoint == 0)
                {
                    currWaypoint = 1;
                }
                else
                {
                    currWaypoint = 0;
                }
                agent.SetDestination(waypoints[currWaypoint].transform.position);
                break;
            case State.ChaseTarget:
                //Debug.Log("chasing");
                agent.SetDestination(target.transform.position);
                //Debug.Log(agent.transform.position);
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("jsdklfhalkdjs");
        /*if (col.gameObject.tag == "WaterPower")
        {
            anim.SetBool("isDead", true);
            //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //Debug.Log("hit by water");
            Destroy(agent, 2);
        }*/
        if (col.gameObject.tag == "River")
        {
            //Debug.Log("Enemy Death triggered");
            anim.SetBool("isDead", true);
            //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(agent, 0.1f);
            Destroy(gameObject, 2);
        }
       /* Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            //crash = true;
            Debug.Log("hit by enemy");
            //animator.SetBool("crashEnemy", true);
            //SceneManager.LoadScene("Death");
        }*/
    }

    private void FindTarget()
    {
/*        float targetRangeIdle = 300f;

        if (Vector3.Distance(transform.position, target.transform.position) < targetRangeIdle)
        {
            state = State.Patrolling;
        }*/

        float targetRangeChase= 15f;
        //Debug.Log("FindTarget");
        Debug.Log(targetRangeChase);
        Debug.Log(target.transform.position);
        Debug.Log(transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < targetRangeChase)
        {
            //Debug.Log("InRange");
            state = State.ChaseTarget;
            setNextWaypoint();
        }
    }

   
}

/*
public class enemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    float velocityZ = 0.0f;
    float velocityX = 0.0f;

    //for patrolling state 
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    //for attacking state 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //state set up 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public float health;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {

        animator.SetBool("isIdle", true);

        //Check where player is 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chasing();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Attacking();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkpoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        //when reached 
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        // make sure enemy doesn't move 
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Projectile attack code 


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
 }
*/