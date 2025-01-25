using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    [SerializeField] private float attackRange;
    [SerializeField] private bool canSeeTarget = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 agentPos = agent.transform.position;
        Vector3 targetPos = target.transform.position;
        if (canSeeTarget)
        {
            float distance = Vector3.Distance(agentPos, targetPos);
            if (distance < attackRange) 
            {
                AttackTarget();
            }
            else 
            {
                FollowTarget();
            }
        }
        else
        {
            Wander();
        }

        if (Physics.Raycast(agentPos, targetPos - agentPos, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.tag);
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(agentPos, targetPos, Color.green);
            }
            else 
            {
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
}
