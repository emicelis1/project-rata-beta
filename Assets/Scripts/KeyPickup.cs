using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool _isRedKey, _isBlueKey, _isGreenKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isRedKey)
            {
                other.GetComponent<PlayerInventory>()._hasRed = true;
            }

            if (_isBlueKey)
            {
                other.GetComponent<PlayerInventory>()._hasBlue = true;
            }

            if (_isGreenKey)
            {
                other.GetComponent<PlayerInventory>()._hasGreen = true;
            }

            Destroy(gameObject);
        }
    }
}
