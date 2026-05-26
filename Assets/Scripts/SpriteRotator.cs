using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private bool _doomMode = true;
    [SerializeField] private float _spinSpeed = 100f;

    void LateUpdate()
    {
        if (_doomMode && Camera.main != null)
            transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
        else
            transform.Rotate(Vector3.up * _spinSpeed * Time.deltaTime);
    }
}

