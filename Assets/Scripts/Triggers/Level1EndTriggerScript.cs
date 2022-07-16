using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1EndTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerContainer"))
        {
            GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().Win();
        }
    }
}
