using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public int weaponIndex; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager wm = other.GetComponent<WeaponManager>();
            if (wm != null)
            {
                wm.UnlockWeapon(weaponIndex);
                Destroy(gameObject); 
            }
        }
    }
}