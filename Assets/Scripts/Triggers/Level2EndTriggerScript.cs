using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2EndTriggerScript : MonoBehaviour
{
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().objective = true;
            Destroy(gameObject);
        }
    }
}
