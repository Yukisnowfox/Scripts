using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Enemy enemy;

    private int wavepointIndex = 0;
    private Transform target;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        // Disable agent avoidance to allow agents to pass through each other
        agent.avoidancePriority = 0;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        
        // Set initial target
        target = Waypoints.points[wavepointIndex];
        agent.SetDestination(target.position);

        // Set the speed of the agent based on the enemy's speed
        agent.speed = enemy.speed;

        agent.acceleration = 9999f; // Essentially instant acceleration

        agent.angularSpeed = 10000f;  // Essentially instant turning

        agent.stoppingDistance = 0f;
    }

    void Update()
    {
        // Update the speed of the agent in case it changes (e.g., slow effects)
        agent.speed = enemy.speed;

        // Check if the agent is close to the current waypoint
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GetNextWaypoint();
        }

        // Reset enemy speed for any potential modifiers
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
        agent.SetDestination(target.position);
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}