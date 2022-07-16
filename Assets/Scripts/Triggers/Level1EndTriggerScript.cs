using UnityEngine;

public class Level1EndTriggerScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerContainer"))
        {
            GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().Win();
        }
    }
}
