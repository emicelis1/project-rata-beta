using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;

    public float bigDamage = 2f;
    public float smallDamage = 1f;


    public float fireRate = 1f;
    private float nextTimeToFire;

    private BoxCollider gunTrigger;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;
    public EnemyManager enemyManager;

    public Animator weaponAnimator;
    public Sprite weaponIcon;

    void OnEnable()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);

        if (weaponAnimator == null)
        {
            weaponAnimator = GetComponentInChildren<Animator>();
        }

        if (CanvasManager.Instance != null && weaponIcon != null)
        {
            CanvasManager.Instance.UpdateGearIndicator(weaponIcon);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Slash");
        }

        foreach (var enemyCollider in enemyColliders)
        {
            
            if (enemyCollider != null && enemyCollider.GetComponent<EnemyAwareness>() != null)
            {
                enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
            }
        }

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            
            if (enemy == null)
            {
                continue;
            }

            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        enemy.TakeDamage(bigDamage);
                    }
                }
            }
        }

        nextTimeToFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {        
        Enemy enemy = other.GetComponent<Enemy>();
       
        if (enemy != null && enemyManager != null)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {       
        Enemy enemy = other.GetComponent<Enemy>();
        
        if (enemy != null && enemyManager != null)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
