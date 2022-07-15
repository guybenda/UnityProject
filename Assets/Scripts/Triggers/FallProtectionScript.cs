using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallProtectionScript : MonoBehaviour
{
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Coll " + other.gameObject.name);
            GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        }
    }
}
