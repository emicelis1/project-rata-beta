using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretItem : MonoBehaviour
{
    [Tooltip("Easter_Egg_Practica")]
    public string secretID;

    private void Start()
    {
        // Si ya fue recogido el Easter Egg, se destruye y en la siguiente escena se carga:
        if (PlayerPrefs.GetInt(secretID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickEasterEgg();
        }
    }

    void PickEasterEgg()
    {
        // Se guarda el ID en el int del objeto
        PlayerPrefs.SetInt(secretID, 1);
        PlayerPrefs.Save(); 

        Debug.Log("Easter Egg encontrado" + secretID);     
        Destroy(gameObject);
    }
}

