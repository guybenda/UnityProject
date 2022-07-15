using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallProtectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject.GetComponentInParent<EnemyScript>().gameObject);
        }
    }
}
