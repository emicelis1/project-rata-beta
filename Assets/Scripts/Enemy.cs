using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyManager enemyManager;
    private float enemyHeath = 2f;

    public GameObject gunHitEffect;
    void Start()
    {

    }


    void Update()
    {
        if (enemyHeath <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHeath -= damage;
    }
}
