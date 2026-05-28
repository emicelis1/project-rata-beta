using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI weapon;

    public Image healthIndicator;
    public Image GearIndicator;

    public Sprite health1; // max health
    public Sprite health2;
    public Sprite health3;
    public Sprite health4; // dead

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;

    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString();
        UpdateHealthIndicator(healthValue);
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString();
    }

    public void UpdateHealthIndicator(int healthValue)
    {
        if (healthValue >= 66)
        {
            healthIndicator.sprite = health1;
        }

        if (healthValue < 66 && healthValue >= 33)
        {
            healthIndicator.sprite = health2;
        }

        if (healthValue < 33 && healthValue > 0)
        {
            healthIndicator.sprite = health4;
        }

        if (healthValue <= 0)
        {
            healthIndicator.sprite = health4;
        }
    }


    public void UpdateGearIndicator()
    {

    }

    public void UpdateKeys(string keyColor)
    {
        if (keyColor == "red")
        {
            redKey.SetActive(true);
        }

        if (keyColor == "green")
        {
            greenKey.SetActive(true);
        }

        if (keyColor == "green")
        {
            blueKey.SetActive(true);
        }
    }

    public void ClearKeys()
    {
        redKey.SetActive(false);
        greenKey.SetActive(false);
        blueKey.SetActive(false);
    }
}
