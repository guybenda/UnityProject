using UnityEngine;

public class FallProtectionScript : MonoBehaviour
{
    public Transform spawn;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        }
    }
}
