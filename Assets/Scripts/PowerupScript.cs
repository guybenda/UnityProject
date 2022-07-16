using UnityEngine;

public enum PowerupType
{
    Tripleshot,
    Health
}

public class PowerupScript : MonoBehaviour
{
    public PowerupType type;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 10f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().AddPowerUp(type);

            Destroy(gameObject);
        }
    }
}
