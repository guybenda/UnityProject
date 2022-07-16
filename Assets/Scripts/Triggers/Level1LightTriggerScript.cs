using System.Linq;
using UnityEngine;

public class Level1LightTriggerScript : MonoBehaviour
{
    public GameObject obj;

    void Start()
    {
        obj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerContainer")) return;

        GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().objective = true;
        obj.SetActive(true);

        var explosionSource = GameObject.FindGameObjectWithTag("ExplosionSource");
        var wallParts = GameObject.FindGameObjectsWithTag("WallParts").Select(g =>
        {
            g.GetComponent<Level1ColliderWallScript>().health = 3;
            return g.GetComponent<Rigidbody>();
        }).ToArray();

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
