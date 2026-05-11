using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] private bool isHealth;
    [SerializeField] private bool isArmor;
    [SerializeField] private bool isAmmo;

    [SerializeField] private int amount;

    void Start()
    {

    }

    
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if (isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject);
            }
            if (isAmmo)
            {
                
            }
        }
    }
}
