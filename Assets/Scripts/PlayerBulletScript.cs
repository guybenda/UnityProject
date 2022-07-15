using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float killTime = 3f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 9;

        StartCoroutine(AddCollisionRoutine());

        Destroy(gameObject, killTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9) return;

        //Debug.Log("AM COLLIDE!! " + collision.collider.name);

        if (collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemy))
        {
            if (enemy.health <= 0) return;

            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

    IEnumerator AddCollisionRoutine()
    {
        yield return new WaitForSeconds(0.05f);

        var collider = gameObject.AddComponent<SphereCollider>();
        collider.center = Vector3.zero;
        collider.radius = 0.25f;
    }
}
