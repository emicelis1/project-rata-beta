using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Definimos los tipos de enemigos
    public enum EnemyType { Melee, Ranged }

    [Header("Variante de Enemigo")]
    public EnemyType typeOfEnemy = EnemyType.Melee;

    [Header("Configuración de Ataque Ranged")]
    public GameObject projectilePrefab; // El proyectil que va a disparar
    public Transform firePoint;          // Punto desde donde sale el disparo (un objeto vacío en el enemigo)
    public float fireRate = 1.5f;        // Cada cuántos segundos dispara
    private float nextFireTime;

    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private Enemy enemyScript; // Referencia para obtener el daño

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindFirstObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();
    }

    private void Update()
    {
        // Si el enemigo no se ha percatado del jugador, no hace nada
        if (!enemyAwareness.isAggro)
        {
            if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
            {
                enemyNavMeshAgent.SetDestination(transform.position);
            }
            return;
        }

        // Lógica según el tipo de variante
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
        // Nos aseguramos de que no se mueva (frenamos el agente)
        if (enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh)
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }

        // Lógica de temporizador para disparar
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Creamos el proyectil en el firePoint
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Calculamos la dirección hacia el pecho/centro del jugador (asumiendo su posición)
            Vector3 targetDir = (playersTransform.position - firePoint.position).normalized;

            // Si el proyectil tiene un componente propio para moverse, le pasamos los datos
            EnemyProjectile projectileScript = proj.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                // Le pasamos el daño asignado en el script Enemy para mantener consistencia
                int damage = enemyScript != null ? enemyScript.attackDamage : 10;
                projectileScript.Setup(targetDir, damage);
            }
        }
    }
}