using UnityEngine;

public class FallProtectionScript : MonoBehaviour
{
    public Transform spawn;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Coll " + other.gameObject.name);
            var player = GameObject.FindGameObjectWithTag("Player");
            var charController = player.GetComponent<CharacterController>();

            charController.enabled = false;
            player.transform.position = spawn.position;
            charController.enabled = true;
        }
    }
}
