using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VictimBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    private Animator anim;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States 
    public float sightRange, attackRange;
    public bool targetInSightRange, targetInAttackRange;
    private bool hasBeenDoubled = false;


    public void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!targetInSightRange && !targetInAttackRange)
        {
            Stop();
        }
        else if (targetInSightRange && !targetInAttackRange)
        {
            Chase();
        }
        else
        {
            Stop();
        }

    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Stop()
    {
        anim.SetBool("InRangeDistant", true);
        agent.SetDestination(transform.position);
        transform.LookAt(player);
    }


    private void Chase()
    {
        anim.SetBool("InRangeDistant", false);
        agent.SetDestination(player.position);
        if(!hasBeenDoubled)
        {
            FindObjectOfType<EnemyAI>().triplePatrollingSpeed();
            hasBeenDoubled = true;
        }
        
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    public void TakeDamage(int damage)
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
