using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
 
    public enum EnemyType { Melee, Ranged, Centipede }

    [Header("Enemy Variant")]
    public EnemyType typeOfEnemy = EnemyType.Melee;

    [Header("Ranged Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime;

    [Header("Configuración de Ruta (Ciempiés)")]
    public Transform[] waypoints;        // Arrastra aquí los puntos por donde caminará
    public float waypointThreshold = 0.5f; // Qué tan cerca debe estar del punto para ir al siguiente
    private int currentWaypointIndex = 0;

    [Header("Línea de Visión")]
    public LayerMask visionObstacleLayers; 

    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private Enemy enemyScript;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindFirstObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();

        
        if (visionObstacleLayers == 0)
        {
            visionObstacleLayers = LayerMask.GetMask("Default", "MapTerrain"); 
        }
    }

    private void Update()
    {
        
        if (typeOfEnemy == EnemyType.Centipede)
        {
            
            ControlCentipedeMovement();
            return;
        }

        
        if (!enemyAwareness.isAggro)
        {
            if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
            {
                enemyNavMeshAgent.SetDestination(transform.position);
            }
            return;
        }

        switch (typeOfEnemy)
        {
            case EnemyType.Melee:
                ControlMeleeMovement();
                break;

            case EnemyType.Ranged:
                ControlRangedAttack();
                break;
        }
    }

    private void ControlMeleeMovement()
    {
        if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }
    }

    private void ControlRangedAttack()
    {
        if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }

        if (Time.time >= nextFireTime)
        {
            
            if (CanSeePlayer())
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void ControlCentipedeMovement()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
        {
            
            enemyNavMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

            
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

            
            if (distanceToWaypoint <= waypointThreshold)
            {
                
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    
    private bool CanSeePlayer()
    {
        if (playersTransform == null || firePoint == null) return false;

       
        Vector3 targetDir = (playersTransform.position - firePoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(firePoint.position, playersTransform.position);

        
        if (Physics.Raycast(firePoint.position, targetDir, out RaycastHit hit, distanceToPlayer, visionObstacleLayers))
        {
       
            return false;
        }

        
        return true;
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 targetDir = (playersTransform.position - firePoint.position).normalized;

            EnemyProjectile projectileScript = proj.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                int damage = enemyScript != null ? enemyScript.attackDamage : 10;
                projectileScript.Setup(targetDir, damage);
            }
        }
    }
}