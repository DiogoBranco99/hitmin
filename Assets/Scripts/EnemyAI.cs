using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsPlayer2;
    public float health;
    public GameObject ballGameObject;
    public bool isPaused = false;
    public float damageDone;
    private Transform location;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float patrollingSpeed;
    public float slowedSpeed;
    public float chasingSpeed;
    public float stunTime;


    //States 
    public float sightRange, attackRange;
    public bool targetInSightRange, targetInAttackRange, playerInSightRange;
    public GameObject combatMusic;
    public GameObject backgroundMusic;
    private AudioSource combatMusicAudio;
    private AudioSource backgroundMusicAudio;

    public void Start()
    {
        combatMusicAudio = combatMusic.GetComponent<AudioSource>();
        backgroundMusicAudio = backgroundMusic.GetComponent<AudioSource>();
        combatMusicAudio.Pause();
    }

    public Transform playerFPS;
    //private bool hasPlayed = false;
    public bool isSlowed = false;

    public void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!targetInSightRange && !targetInAttackRange)
        {
            Patroling();
        }
        else if (targetInSightRange && !targetInAttackRange)
        {
            Chase();
        }
        else
        {
            if (!isPaused)
            {
                Attack();
            }
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer2);

        if (playerInSightRange)
        {
            backgroundMusicAudio.Pause();
            combatMusicAudio.UnPause();
            //hasPlayed = true;
        }
        else if (!playerInSightRange)
        {
            backgroundMusicAudio.UnPause();
            combatMusicAudio.Pause();
            //hasPlayed = false;
        }
    }

    private void Awake()
    {
        player = GameObject.Find("NPCVictim").transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = GameObject.Find("Enemy").transform;
        location = GameObject.Find("Arm03").transform;
    }

    private void Patroling()
    {
        agent.SetDestination(player.position);
        agent.speed = patrollingSpeed;
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        if (isSlowed)
        {
            agent.speed = slowedSpeed;
        } else
        {
            agent.speed = chasingSpeed;
        }
        
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            ThrowBallAtTargetLocation(player.position, 10f);
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

    public void StunOrSlow(bool doesStun)
    {

        if (doesStun)
        {
            // stun agent here
            agent.isStopped = true;

            // wait x seconds and invoke unstun
            Invoke("unstun", stunTime);
        } else
        {
            isSlowed = true;

            // wait x seconds and invoke restore speed
            Invoke("restoreSpeed", 3);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public float getAttackRange() { return attackRange; }

    public void ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    {
        Vector3 direction = (targetLocation - location.position).normalized;
        float distance = Vector3.Distance(targetLocation, transform.position);

        Vector3 elevation = Quaternion.AngleAxis(90, location.right) * location.up;
        float directionAngle = AngleBetweenAboutAxis(location.forward, direction, location.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, location.up) * elevation * initialVelocity;

        GameObject newSphere = Instantiate(ballGameObject, location.position + location.forward, Quaternion.identity);
        // ballGameObject is object to be thrown
        newSphere.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
        player.GetComponent<NPCHealth>().TakeDamage(damageDone);
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

    public void unstun()
    {
        agent.isStopped = false;
    }

    public void restoreSpeed()
    {
        isSlowed = false;
    }

    public void triplePatrollingSpeed()
    {
        patrollingSpeed *= 3;
    }

}
