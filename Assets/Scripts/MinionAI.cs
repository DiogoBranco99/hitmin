using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    private Transform enemy;
    public GameObject ballGameObject;
    public bool isPaused = false;
    public float damageDone;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States 
    public float sightRange, attackRange;
    public bool targetInSightRange, targetInAttackRange;

    //Sounds
    AudioSource damageSound;

    void Start()
    {
        damageSound = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!targetInSightRange && !targetInAttackRange)
        {
            Idling();
        }
        else if (targetInSightRange && !targetInAttackRange)
        {
            Chase();
        }
        else
        {
            if(!isPaused)
            {
                Attack();
            }
                
        }
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = gameObject.transform;
    }

    private void Idling()
    {

    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            ThrowBallAtTargetLocation(player.position, 10f);
            damageSound.Play();
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    {
        Vector3 direction = (targetLocation - enemy.position).normalized;
        float distance = Vector3.Distance(targetLocation, transform.position);

        Vector3 elevation = Quaternion.AngleAxis(90, enemy.right) * enemy.up;
        float directionAngle = AngleBetweenAboutAxis(enemy.forward, direction, enemy.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, enemy.up) * elevation * initialVelocity;


        GameObject newSphere = Instantiate(ballGameObject, enemy.position, Quaternion.identity);
        // ballGameObject is object to be thrown
        newSphere.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
        player.GetComponent<PlayerMovement>().Slow();
        Destroy(newSphere, 1f);
    }

    // Helper method to find angle between two points (v1 & v2) with respect to axis n
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    public void setgameIsPaused(bool condition)
    {
        isPaused = condition;
    }


}
