using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    public enum EnemyType { Melee, Ranged, Centipede }

    [Header("Variante de Enemigo")]
    public EnemyType typeOfEnemy = EnemyType.Melee;

    [Header("Configuración de Ataque Ranged")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime;

    [Header("Configuración de Ruta (Ciempiés)")]
    public Transform[] waypoints;        // Arrastra aquí los puntos por donde caminará
    public float waypointThreshold = 0.5f; // Qué tan cerca debe estar del punto para ir al siguiente
    private int currentWaypointIndex = 0;

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
    }

    private void Update()
    {
        // CONTROL ESPECIAL: Si es un ciempiés, ejecuta su lógica directo e ignora la detección
        if (typeOfEnemy == EnemyType.Centipede)
        {
            ControlCentipedeMovement();
            return;
        }

        // Lógica normal para Melee y Ranged (requieren detección)
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
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ControlCentipedeMovement()
    {
        // Si no hay puntos definidos, no hace nada
        if (waypoints == null || waypoints.Length == 0) return;

        if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
        {
            // Le decimos al NavMeshAgent que vaya al waypoint actual
            enemyNavMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

            // Verificamos la distancia entre el enemigo y el waypoint actual
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

            // Si ya llegó al punto cercano, pasa al siguiente
            if (distanceToWaypoint <= waypointThreshold)
            {
                // Incrementa el índice y vuelve a 0 si llega al final de la lista (Bucle continuo)
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
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