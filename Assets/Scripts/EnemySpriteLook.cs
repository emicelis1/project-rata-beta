using UnityEngine;

public class EnemySpriteLook : MonoBehaviour
{
    private Transform _target;
    public bool canLookVertically;

    void Start()
    {
        _target = FindFirstObjectByType<PlayerMove>().transform;
    }

    void Update()
    {
        if (canLookVertically)
        {
            transform.LookAt(_target);
        }
        else
        {
            Vector3 modifiedTarget = _target.position;
            modifiedTarget.y = transform.position.y;

            transform.LookAt(modifiedTarget);
        }

    }
}
