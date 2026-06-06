using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> allWeapons; 
    public bool[] unlockedWeapons;    
    private int currentWeaponIndex = 0;

    void Start()
    {
        
        unlockedWeapons[0] = true;  
        unlockedWeapons[1] = false; 
        unlockedWeapons[2] = false; 

        for (int i = 0; i < allWeapons.Count; i++)
        {
            allWeapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) TryChangeWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) TryChangeWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TryChangeWeapon(2);

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon(-1); 
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchWeapon(1);  
        }
    }

    public void UnlockWeapon(int index)
    {
        if (index < unlockedWeapons.Length)
        {
            unlockedWeapons[index] = true;
            ChangeWeapon(index); 
        }
    }

    void SwitchWeapon(int direction)
    {
        
        if (allWeapons.Count == 0) return;

        int nextWeapon = currentWeaponIndex;
        bool foundValidWeapon = false;

        
        for (int i = 0; i < allWeapons.Count; i++)
        {
            nextWeapon += direction;

            
            if (nextWeapon >= allWeapons.Count) nextWeapon = 0;
            if (nextWeapon < 0) nextWeapon = allWeapons.Count - 1;

            
            if (nextWeapon < unlockedWeapons.Length && unlockedWeapons[nextWeapon])
            {
                foundValidWeapon = true;
                break; 
            }
        }

        if (foundValidWeapon && nextWeapon != currentWeaponIndex)
        {
            ChangeWeapon(nextWeapon);
        }
    }

    void ChangeWeapon(int newIndex)
    {
        allWeapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = newIndex;
        allWeapons[currentWeaponIndex].SetActive(true);
        UpdateUI();
    }
    void TryChangeWeapon(int index)
    {
        
        if (index >= 0 && index < allWeapons.Count && unlockedWeapons[index])
        {
            ChangeWeapon(index);
        }
    }

    void UpdateUI()
    {
        
        Gun currentGun = allWeapons[currentWeaponIndex].GetComponent<Gun>();
        if (CanvasManager.Instance != null && currentGun.weaponIcon != null)
        {
            CanvasManager.Instance.UpdateGearIndicator(currentGun.weaponIcon);
        }
    }


}