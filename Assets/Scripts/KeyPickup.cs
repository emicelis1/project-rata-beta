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
                CanvasManager.Instance.UpdateKeys(keyColor:"red");
            }

            if (_isBlueKey)
            {
                other.GetComponent<PlayerInventory>()._hasBlue = true;
                CanvasManager.Instance.UpdateKeys(keyColor:"blue");
            }

            if (_isGreenKey)
            {
                other.GetComponent<PlayerInventory>()._hasGreen = true;
                CanvasManager.Instance.UpdateKeys(keyColor:"green");
            }

            Destroy(gameObject);
        }
    }
}
