using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    Tripleshot,
    Health
}

public class PowerupScript : MonoBehaviour
{
    public PowerupType type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
