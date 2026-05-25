using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator _doorAnim;
    public GameObject _areaToSpawn;

    public bool _requiresKey;
    public bool _reqRed, _reqGreen, _reqBlue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_requiresKey)
            {

                if (_reqRed && other.GetComponent<PlayerInventory>()._hasRed)
                {
                    _doorAnim.SetTrigger("OpenDoor");
                    _areaToSpawn.SetActive(true);
                }

                if (_reqBlue && other.GetComponent<PlayerInventory>()._hasBlue)
                {
                    _doorAnim.SetTrigger("OpenDoor");
                    _areaToSpawn.SetActive(true);
                }

                if (_reqGreen && other.GetComponent<PlayerInventory>()._hasGreen)
                {
                    _doorAnim.SetTrigger("OpenDoor");
                    _areaToSpawn.SetActive(true);
                }
            }
            else
            {
                _doorAnim.SetTrigger("OpenDoor");
                _areaToSpawn.SetActive(true);
            }

            // abrir puerta

 
        }
    }

}
