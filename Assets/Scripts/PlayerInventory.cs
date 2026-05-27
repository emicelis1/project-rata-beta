using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool _hasRed, _hasGreen, _hasBlue;


    private void Start()
    {
        CanvasManager.Instance.ClearKeys();
    }
}
