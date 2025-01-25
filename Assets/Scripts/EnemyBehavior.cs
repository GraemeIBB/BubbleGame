using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject enemy;
    public GameObject target;
    [SerializeField] private float attackRange;
    [SerializeField] private bool canSeeTarget = true;

    void Update()
    {
        if (canSeeTarget)
        {
            float distance = Vector3.Distance(agent.transform.position, target.transform.position);
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
