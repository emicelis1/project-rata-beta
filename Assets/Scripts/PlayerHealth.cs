using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    private int health;

    public int maxArmor;
    private int armor;


    void Start()
    {
        health = maxHealth;
       // solo para testear
    }


    void Update()
    {
        // temporary test function
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            DamagePlayer(30);
            Debug.Log("Player has recieved damage");
        }
    }

    public void DamagePlayer(int damage)
    {

        if (armor > 0)
        {
            // damage armor

            if (armor >= damage)
            {
                armor -= damage;
            }
            else if (armor < damage)
            {
                int remainingDamage;

                remainingDamage = damage - armor;

                armor = 0;

                health -= remainingDamage;
            }

        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            // you're dead

            Debug.Log("Player is dead");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }

        if (armor > maxArmor)
        {
            armor = maxArmor;
        }
    }
}
