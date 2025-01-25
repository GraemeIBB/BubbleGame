using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    public bool isDead = false;

    private bool canSeeTarget;
    private bool hasDied = false;
    [SerializeField] private float attackRange;
    [SerializeField] private float visionRange;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float deathForce = 10.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindWithTag("Player");

            if (target == null)
            {
                Debug.LogError("No player object found!! Please insert a player into the scene");
            }        
        }
    }

    void Update()
    {
        if (hasDied) return; // this is to prevent trying to delete the object twice (there's probably a better way to do this)
        if (health <= 0 || isDead || transform.position.y < -100.0f)
        {
            hasDied = true;
            StartCoroutine(Die());
            return;
        }

        Vector3 agentPos = agent.transform.position;
        Vector3 targetPos = target.transform.position;
        if (Physics.Raycast(agentPos, targetPos - agentPos, out RaycastHit hit))
        {
            // Debug.Log("Hit: " + hit.collider.tag);
            float distanceFromTarget = Vector3.Distance(agentPos, targetPos);

            // If the enemy can see the player and are in vision range
            if (canSeeTarget || (hit.collider.CompareTag("Player") && distanceFromTarget <= visionRange))
            {
                canSeeTarget = true;

                // attack in attack range
                if (distanceFromTarget < attackRange)
                {
                    AttackTarget();
                }
                else
                {
                    FollowTarget();
                }
                Debug.DrawLine(agentPos, targetPos, Color.green);
            }
            else 
            {
                Wander();
                Debug.DrawLine(agentPos, targetPos, Color.red);
            }
        }
    }

    private void AttackTarget() 
    {
        // do something here
        agent.isStopped = true;
        agent.ResetPath();
    }

    private void FollowTarget() 
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    private void Wander()
    {
        // randomly move around
        agent.isStopped = true;
        agent.ResetPath();
    }

    private IEnumerator Die()
    {
        agent.isStopped = true;
        agent.ResetPath();
        gameObject.GetComponent<Rigidbody>().AddForce(-deathForce * transform.forward);

        Destroy(agent);
         
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasDied) return;

        if (other.gameObject.CompareTag("Bubble"))
        {
            health -= other.gameObject.GetComponent<bubble_behaviour>().damage;
            Destroy(other.gameObject);
            Debug.Log("New Health: " + health);
        }
    }

    // public methods ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }
}
