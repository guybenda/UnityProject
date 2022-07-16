using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBoxScript : MonoBehaviour
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
        if (collision.gameObject.layer == 9)
        {
            //Debug.Log("AM COLLIDE!! " + collision.collider.name);

            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 8f)
                {
                    enemy.GetComponent<EnemyScript>().Damage(100);
                }
            }

            GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(ExplodeRoutine());
        }
    }

    IEnumerator ExplodeRoutine()
    {
        var light = GetComponent<Light>();
        bool done = false;

        light.enabled = true;

        while (!done)
        {
            yield return new WaitForFixedUpdate();
            light.intensity -= 20;
            done = light.intensity <= 0;
        }

        Destroy(gameObject);
    }
}
