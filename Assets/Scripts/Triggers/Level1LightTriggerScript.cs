using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        if (!other.CompareTag("Player")) return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().objective = true;
        obj.SetActive(true);

        var explosionSource = GameObject.FindGameObjectWithTag("ExplosionSource");
        var wallParts = GameObject.FindGameObjectsWithTag("WallParts").Select(g => g.GetComponent<Rigidbody>()).ToArray();

        foreach (var part in wallParts)
        {
            part.constraints = RigidbodyConstraints.None;
        }

        foreach (var part in wallParts)
        {
            part.AddExplosionForce(2500f, explosionSource.transform.position, 100f);
        }

        Destroy(gameObject);
    }
}
