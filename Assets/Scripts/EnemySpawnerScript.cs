using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject from;
    public GameObject to;
    public GameObject enemy;
    public GameObject container;
    public float delay = 0.3f;
    public int maxEnemies = 30;

    private float currentDelay = 0f;
    private Transform[] waypoints;
    private Material[] masks;

    // Start is called before the first frame update
    void Start()
    {
        currentDelay = delay;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").Select(g => g.transform).ToArray();
        masks = Resources.LoadAll<Material>("Masks");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        currentDelay -= Time.fixedDeltaTime;

        if (currentDelay <= 0f)
        {
            currentDelay += delay;
            if (container.transform.childCount < maxEnemies) CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
        var position = new Vector3(
            Random.Range(from.transform.position.x, to.transform.position.x),
            0f,
            Random.Range(from.transform.position.z, to.transform.position.z)
        );

        position.y = Terrain.activeTerrain.SampleHeight(position);
        Debug.Log(position.y);
        enemy = Instantiate(enemy, position, Quaternion.identity, container.transform);

        enemy.GetComponent<EnemyScript>().waypoints = waypoints;
        enemy.GetComponentInChildren<SkinnedMeshRenderer>().material = masks[Random.Range(0, masks.Length)];
    }
}
