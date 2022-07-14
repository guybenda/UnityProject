using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1LightTriggerScript : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(true);
        }
    }
}
